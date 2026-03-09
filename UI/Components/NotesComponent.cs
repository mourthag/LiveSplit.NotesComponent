using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using LiveSplit.Model;
using LiveSplit.Utility;

namespace LiveSplit.UI.Components
{
    /// <summary>
    /// LiveSplit component that displays per-split notes read from a text file.
    /// </summary>
    /// <remarks>
    /// File format (high level):
    /// - Sections are delimited by lines that contain "----------".
    /// - Inside a section, an optional header line "!!!SplitName" binds the section to a split.
    /// - Lines may start with a 3-char marker (e.g. ">>>", "###") to set a note type.
    /// - Sections without "!!!" can be mapped either to the first splits (leading sections)
    ///   or to splits following the last explicit "!!!" header.
    /// </remarks>
    internal class NotesComponent : IComponent
    {
        /// <summary>
        /// Settings control for this component (file path, colors, etc.).
        /// </summary>
        public NotesComponentSettings Settings { get; set; }

        /// <summary>
        /// The currently active node (notes section) to be rendered.
        /// </summary>
        public SplitNotes CurrentNode { get; protected set; }

        /// <summary>
        /// Reusable empty node to avoid re-allocations / flicker when a split has no notes.
        /// </summary>
        private readonly SplitNotes _emptyNode;

        /// <summary>
        /// Path of the debug log file (written next to the notes file).
        /// </summary>
        private string _debugLogPath;

        /// <summary>
        /// Tracks the last split logged to avoid flooding the log on every frame.
        /// </summary>
        private ISegment _lastLoggedSplit;

        /// <summary>
        /// True only for the first Update() call after a split change — gates all Log() calls.
        /// </summary>
        private bool _shouldLog;

        /// <summary>
        /// Appends a timestamped line to the debug log.
        /// </summary>
        private void Log(string msg)
        {
            try
            {
                if (!_shouldLog || !Settings.DebugLogging || string.IsNullOrEmpty(_debugLogPath)) return;
                File.AppendAllText(_debugLogPath, $"[{DateTime.Now:HH:mm:ss.fff}] {msg}\r\n");
            }
            catch { /* never crash LiveSplit */ }
        }

        /// <summary>
        /// All parsed note sections from the notes file.
        /// </summary>
        private readonly List<SplitNotes> _loadedNotes = new List<SplitNotes>();

        public string ComponentName => "Notes";

        public IDictionary<string, Action> ContextMenuControls => null;

        /// <summary>
        /// Creates the component and loads the notes file.
        /// </summary>
        /// <param name="state">Current LiveSplit state.</param>
        public NotesComponent(LiveSplitState state)
        {
            Settings = new NotesComponentSettings(state)
            {
                CurrentState = state
            };

            _emptyNode = new SplitNotes(new List<string>(), Settings);
            ReadFile();
        }

        public float HorizontalWidth => CurrentNode.HorizontalWidth;

        public float MinimumHeight => CurrentNode.MinimumHeight;

        public float MinimumWidth => CurrentNode.MinimumWidth;

        public float PaddingBottom => CurrentNode.PaddingBottom;

        public float PaddingLeft => CurrentNode.PaddingLeft;

        public float PaddingRight => CurrentNode.PaddingRight;

        public float PaddingTop => CurrentNode.PaddingTop;

        public float VerticalHeight => CurrentNode.VerticalHeight;

        public void Dispose()
        {
            foreach (var node in _loadedNotes)
            {
                foreach (var comp in node.VisibleComponents ?? Enumerable.Empty<IComponent>())
                    comp.Dispose();
            }

            _loadedNotes.Clear();
        }

        public void DrawHorizontal(Graphics g, LiveSplitState state, float height, Region clipRegion)
        {
            CurrentNode.DrawHorizontal(g, state, height, clipRegion);
        }

        public void DrawVertical(Graphics g, LiveSplitState state, float width, Region clipRegion)
        {
            CurrentNode.DrawVertical(g, state, width, clipRegion);
        }

        public XmlNode GetSettings(XmlDocument document)
        {
            return Settings.GetSettings(document);
        }

        public Control GetSettingsControl(LayoutMode mode)
        {
            Settings.Mode = mode;
            return Settings;
        }

        public void SetSettings(XmlNode settings)
        {
            Settings.SetSettings(settings);
        }

        public void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode)
        {
            if (Settings.FilePathChanged)
                ReadFile();

            // Select the node to show based on the current split.
            CheckCurrentSplit(state);
        }

        /// <summary>
        /// Reads and parses the configured notes file into <see cref="_loadedNotes"/>.
        /// </summary>
        public void ReadFile()
        {
            _loadedNotes.Clear();

            if (string.IsNullOrWhiteSpace(Settings.FilePath))
            {
                CurrentNode = _emptyNode;
                return;
            }

            // Debug log next to the notes file.
            _debugLogPath = Path.Combine(
                Path.GetDirectoryName(Settings.FilePath) ?? "",
                "notes_debug.log");
            if (Settings.DebugLogging)
                try { File.WriteAllText(_debugLogPath, $"=== Log start {DateTime.Now} ===\r\n"); } catch { }

            List<string> lines;
            try
            {
                lines = TxtHelper.readFile(Settings.FilePath);
            }
            catch (Exception)
            {
                CurrentNode = _emptyNode;
                Settings.FilePathChanged = false;
                return;
            }

            if (lines == null || lines.Count == 0)
            {
                CurrentNode = _emptyNode;
                Settings.FilePathChanged = false;
                return;
            }

            int? curSplitStart = null;
            int? curSplitEnd = null;

            int index = 0;

            foreach (string curLine in lines)
            {
                if (curLine.Contains("----------"))
                {
                    if (curSplitStart == null && curSplitEnd == null)
                    {
                        curSplitStart = index + 1;
                    }
                    else if (curSplitStart != null && curSplitEnd == null)
                    {
                        curSplitEnd = index;
                        var curSplitSection = lines.GetRange(curSplitStart.Value, curSplitEnd.Value - curSplitStart.Value);
                        _loadedNotes.Add(new SplitNotes(curSplitSection, Settings));

                        curSplitStart = null;
                        curSplitEnd = null;
                    }
                }
                index++;
            }

            CurrentNode = _loadedNotes.FirstOrDefault() ?? _emptyNode;

            Log($"=== ReadFile done: {_loadedNotes.Count} sections loaded ===");
            for (int i = 0; i < _loadedNotes.Count; i++)
            {
                var n = _loadedNotes[i];
                var label = string.IsNullOrWhiteSpace(n.Name) ? "(unnamed)" : $"!!!{n.Name}";
                Log($"  [{i}] {label}");
            }
            Settings.FilePathChanged = false;
        }

        private static string NormalizeSplitKey(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;

            value = value.Trim();

            var sb = new StringBuilder(value.Length);
            bool inWhitespace = false;

            foreach (char c in value)
            {
                if (char.IsWhiteSpace(c))
                {
                    if (!inWhitespace)
                    {
                        sb.Append(' ');
                        inWhitespace = true;
                    }
                    continue;
                }

                sb.Append(c);
                inWhitespace = false;
            }

            return sb.ToString();
        }

        /// <summary>
        /// Updates <see cref="CurrentNode"/> to match the current split.
        /// Falls back to <see cref="_emptyNode"/> when no notes exist for a split.
        /// </summary>
        /// <param name="state">Current LiveSplit state.</param>
        private void CheckCurrentSplit(LiveSplitState state)
        {
            if (_loadedNotes.Count == 0)
            {
                if (!ReferenceEquals(CurrentNode, _emptyNode))
                    CurrentNode = _emptyNode;
                return;
            }

            if (state.CurrentSplit == null)
            {
                var first = _loadedNotes.FirstOrDefault() ?? _emptyNode;
                if (!ReferenceEquals(CurrentNode, first))
                    CurrentNode = first;
                return;
            }

            var splitKey = NormalizeSplitKey(state.CurrentSplit.Name);

            // Only log once per split change.
            _shouldLog = !ReferenceEquals(state.CurrentSplit, _lastLoggedSplit);
            if (_shouldLog)
            {
                _lastLoggedSplit = state.CurrentSplit;
                Log($"--- CheckCurrentSplit: split='{state.CurrentSplit.Name}' splitKey='{splitKey}'");
            }

            // 1) Exact match via !!!SplitName
            var match = _loadedNotes.FirstOrDefault(x =>
                !string.IsNullOrWhiteSpace(x.Name) &&
                string.Equals(NormalizeSplitKey(x.Name), splitKey, StringComparison.OrdinalIgnoreCase));

            // 2) No explicit match: find the unnamed section that corresponds to this split's
            //    position relative to the nearest preceding named anchor in the run.
            if (match != null)
                Log($"  -> exact match: '{match.Name}'");

            if (match == null && state.Run != null)
            {
                int currentRunIndex = FindCurrentSplitIndex(state);
                Log($"  -> no exact match, currentRunIndex={currentRunIndex}");

                if (currentRunIndex >= 0)
                    match = FindUnnamedSection(currentRunIndex, state);

                Log($"  -> unnamed match: {(match == null ? "null" : $"section at notes index (see below)")}");
            }

            // Final fallback: no notes found for this split.
            var next = match ?? _emptyNode;
            if (!ReferenceEquals(CurrentNode, next))
                CurrentNode = next;
            Log($"  -> CurrentNode set to: {(ReferenceEquals(next, _emptyNode) ? "(emptyNode)" : $"'{next.Name}' (unnamed={string.IsNullOrWhiteSpace(next.Name)})")}");
        }

        /// <summary>
        /// Finds the unnamed notes section for a split at <paramref name="currentRunIndex"/>.
        /// Walks backwards through the run to find the nearest named anchor section,
        /// then counts how many positions after that anchor the current split is,
        /// and returns the unnamed notes section at that ordinal in the file.
        /// </summary>
        private SplitNotes FindUnnamedSection(int currentRunIndex, LiveSplitState state)
        {
            // Walk backwards in the run to find the nearest split that has an explicit !!! section.
            int anchorRunIndex = -1;
            int anchorNotesIndex = -1;

            for (int ri = currentRunIndex - 1; ri >= 0; ri--)
            {
                var candidateKey = NormalizeSplitKey(state.Run[ri]?.Name);
                for (int ni = 0; ni < _loadedNotes.Count; ni++)
                {
                    if (!string.IsNullOrWhiteSpace(_loadedNotes[ni].Name) &&
                        string.Equals(NormalizeSplitKey(_loadedNotes[ni].Name), candidateKey, StringComparison.OrdinalIgnoreCase))
                    {
                        anchorRunIndex = ri;
                        anchorNotesIndex = ni;
                        goto FoundAnchor;
                    }
                }
            }

        FoundAnchor:
            int offset = (anchorRunIndex >= 0)
                ? currentRunIndex - anchorRunIndex
                : currentRunIndex + 1;

            int startNotesIndex = (anchorNotesIndex >= 0) ? anchorNotesIndex + 1 : 0;
            int unnamedCount = 0;

            Log($"  FindUnnamedSection: anchorRunIndex={anchorRunIndex}, anchorNotesIndex={anchorNotesIndex}, offset={offset}, startNotesIndex={startNotesIndex}");

            for (int ni = startNotesIndex; ni < _loadedNotes.Count; ni++)
            {
                if (!string.IsNullOrWhiteSpace(_loadedNotes[ni].Name))
                {
                    Log($"    [ni={ni}] hit named section '{_loadedNotes[ni].Name}', stopping.");
                    break;
                }

                unnamedCount++;
                Log($"    [ni={ni}] unnamed section #{unnamedCount} (need offset={offset})");
                if (unnamedCount == offset)
                {
                    Log($"    -> FOUND at ni={ni}");
                    return _loadedNotes[ni];
                }
            }

            Log($"  FindUnnamedSection: no match found.");
            return null;
        }

        private static int FindCurrentSplitIndex(LiveSplitState state)
        {
            if (state?.Run == null || state.CurrentSplit == null)
                return -1;

            // Preferred: reference equality (fast and unambiguous).
            for (int i = 0; i < state.Run.Count; i++)
            {
                if (ReferenceEquals(state.Run[i], state.CurrentSplit))
                    return i;
            }

            // Fallback: compare by normalized name.
            var currentNameKey = NormalizeSplitKey(state.CurrentSplit.Name);
            if (currentNameKey.Length == 0)
                return -1;

            for (int i = 0; i < state.Run.Count; i++)
            {
                if (string.Equals(
                        NormalizeSplitKey(state.Run[i]?.Name),
                        currentNameKey,
                        StringComparison.OrdinalIgnoreCase))
                    return i;
            }

            return -1;
        }

    }
}

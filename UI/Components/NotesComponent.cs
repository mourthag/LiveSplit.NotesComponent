using System;
using System.Collections.Generic;
using System.Drawing;
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

            // Map sections without !!! to the split(s) after the last explicit !!! section.
            // Additionally: leading unnamed sections (before the first !!!) map to the first splits.
            string lastExplicit = null;
            int afterOffset = 0;
            bool sawExplicit = false;
            int leadingUnnamedIndex = 0;

            foreach (var node in _loadedNotes)
            {
                node.AfterSplitName = null;
                node.AfterSplitOffset = 0;
                node.TargetSplitIndex = null;

                if (!string.IsNullOrWhiteSpace(node.Name))
                {
                    lastExplicit = node.Name;
                    afterOffset = 0;
                    sawExplicit = true;
                    continue;
                }

                // If the file starts with unnamed sections (no !!! yet), map them to the first splits.
                if (!sawExplicit)
                {
                    node.TargetSplitIndex = leadingUnnamedIndex;
                    leadingUnnamedIndex++;
                    continue;
                }

                // After an explicit !!! header: the first unnamed section maps to the next split,
                // the second unnamed section to the split after that, etc.
                if (!string.IsNullOrWhiteSpace(lastExplicit))
                {
                    afterOffset++;
                    node.AfterSplitName = lastExplicit;
                    node.AfterSplitOffset = afterOffset;
                }
            }

            CurrentNode = _loadedNotes.FirstOrDefault() ?? _emptyNode;
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

            // 1) Exact match via !!!SplitName
            var match = _loadedNotes.FirstOrDefault(x =>
                !string.IsNullOrWhiteSpace(x.Name) &&
                string.Equals(NormalizeSplitKey(x.Name), splitKey, StringComparison.OrdinalIgnoreCase));

            // 2) If no explicit match: use section(s) that are defined "after" a split (unnamed sections)
            if (match == null && state.Run != null)
            {
                // Determine the index of the current split in the run.
                int currentIndex = -1;
                for (int i = 0; i < state.Run.Count; i++)
                {
                    if (ReferenceEquals(state.Run[i], state.CurrentSplit))
                    {
                        currentIndex = i;
                        break;
                    }
                }

                if (currentIndex >= 0)
                {
                    // 2a) Leading unnamed sections map directly to split indices
                    match = FindByTargetIndex(currentIndex);

                    // 2b) Unnamed sections after a named anchor
                    if (match == null)
                        match = FindByAfterAnchor(currentIndex, state);
                }
            }

            // Final fallback: no notes found for this split.
            var next = match ?? _emptyNode;
            if (!ReferenceEquals(CurrentNode, next))
                CurrentNode = next;
        }

        /// <summary>
        /// Finds a loaded note section that is directly mapped to the given split index
        /// (leading unnamed sections before the first !!!).
        /// </summary>
        private SplitNotes FindByTargetIndex(int splitIndex)
        {
            foreach (var node in _loadedNotes)
            {
                if (node.TargetSplitIndex.HasValue && node.TargetSplitIndex.Value == splitIndex)
                    return node;
            }
            return null;
        }

        /// <summary>
        /// Finds a loaded note section that is mapped relative to a named anchor split
        /// (unnamed sections that follow a !!! header).
        /// </summary>
        private SplitNotes FindByAfterAnchor(int currentIndex, LiveSplitState state)
        {
            foreach (var node in _loadedNotes)
            {
                if (string.IsNullOrWhiteSpace(node.AfterSplitName) || node.AfterSplitOffset <= 0)
                    continue;

                int anchorIndex = currentIndex - node.AfterSplitOffset;
                if (anchorIndex < 0 || anchorIndex >= state.Run.Count)
                    continue;

                var anchorName = state.Run[anchorIndex]?.Name;
                if (string.Equals(
                    NormalizeSplitKey(anchorName),
                    NormalizeSplitKey(node.AfterSplitName),
                    StringComparison.OrdinalIgnoreCase))
                {
                    return node;
                }
            }
            return null;
        }
    }
}

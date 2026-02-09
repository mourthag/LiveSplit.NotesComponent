using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using LiveSplit.Model;
using System.Drawing.Drawing2D;
using LiveSplit.Utility;
using System.IO;

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
    class NotesComponent : IComponent
    {
        /// <summary>
        /// Settings control for this component (file path, colors, etc.).
        /// </summary>
        public NotesComponentSettings Settings { get; set; }

        /// <summary>
        /// The currently active node (notes section) to be rendered.
        /// </summary>
        public SplitNotes currentNode { get; protected set; }

        /// <summary>
        /// Reusable empty node to avoid re-allocations / flicker when a split has no notes.
        /// </summary>
        private SplitNotes _emptyNode;
        
        /// <summary>
        /// All parsed note sections from the notes file.
        /// </summary>
        List<SplitNotes> loadedNotes = new List<SplitNotes>();

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
            readFile();
        }

        public float HorizontalWidth => currentNode.HorizontalWidth;

        public float MinimumHeight => currentNode.MinimumHeight;

        public float MinimumWidth => currentNode.MinimumWidth;

        public float PaddingBottom => currentNode.PaddingBottom;

        public float PaddingLeft => currentNode.PaddingLeft;

        public float PaddingRight => currentNode.PaddingRight;

        public float PaddingTop => currentNode.PaddingTop;

        public float VerticalHeight => currentNode.VerticalHeight;

        public void Dispose()
        {
        }

        public void DrawHorizontal(Graphics g, LiveSplitState state, float height, Region clipRegion)
        {
            currentNode.DrawHorizontal(g, state, height, clipRegion);
        }

        public void DrawVertical(Graphics g, LiveSplitState state, float width, Region clipRegion)
        {
            currentNode.DrawVertical(g, state, width, clipRegion);
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
                readFile();

            // Select the node to show based on the current split.
            checkCurrentSplit(state);
        }

        /// <summary>
        /// Reads and parses the configured notes file into <see cref="loadedNotes"/>.
        /// </summary>
        public void readFile()
        {
            loadedNotes.Clear();

            if (Settings.FilePath == "")
            {
                currentNode = _emptyNode;
                return;
            }
            List<string> lines = TxtHelper.readFile(Settings.FilePath);

            List<String> curSplitSection = new List<string>();
            
            int? curSplitStart = null;
            int? curSplitEnd  = null;

            int index = 0;

            foreach (string curLine in lines)
            {
                if (curLine.Contains("----------"))
                {
                    if (curSplitStart == null && curSplitEnd == null)
                        curSplitStart = index+1;

                    else if (curSplitStart != null && curSplitEnd == null)
                    {
                        curSplitEnd = index;
                        curSplitSection = lines.GetRange(curSplitStart.Value, curSplitEnd.Value - curSplitStart.Value);

                        loadedNotes.Add(new SplitNotes(curSplitSection, Settings));

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
            foreach (var node in loadedNotes)
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

            currentNode = loadedNotes.FirstOrDefault() ?? _emptyNode;
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
        /// Updates <see cref="currentNode"/> to match the current split.
        /// Falls back to <see cref="_emptyNode"/> when no notes exist for a split.
        /// </summary>
        /// <param name="state">Current LiveSplit state.</param>
        void checkCurrentSplit(LiveSplitState state)
        {
            if (loadedNotes.Count == 0)
            {
                if (!ReferenceEquals(currentNode, _emptyNode))
                    currentNode = _emptyNode;
                return;
            }

            if (state.CurrentSplit == null)
            {
                var first = loadedNotes.FirstOrDefault() ?? _emptyNode;
                if (!ReferenceEquals(currentNode, first))
                    currentNode = first;
                return;
            }

            var splitKey = NormalizeSplitKey(state.CurrentSplit.Name);

            // 1) Exact match via !!!SplitName
            var match = loadedNotes.FirstOrDefault(x =>
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
                    match = loadedNotes.FirstOrDefault(x => x.TargetSplitIndex.HasValue && x.TargetSplitIndex.Value == currentIndex);

                    if (match != null)
                        goto MatchDone;

                    match = loadedNotes.FirstOrDefault(x =>
                    {
                        if (string.IsNullOrWhiteSpace(x.AfterSplitName) || x.AfterSplitOffset <= 0)
                            return false;

                        // "AfterSplitOffset" describes how far the target split is after the anchor split.
                        int anchorIndex = currentIndex - x.AfterSplitOffset;
                        if (anchorIndex < 0 || anchorIndex >= state.Run.Count)
                            return false;

                        var anchorName = state.Run[anchorIndex]?.Name;
                        return string.Equals(
                            NormalizeSplitKey(anchorName),
                            NormalizeSplitKey(x.AfterSplitName),
                            StringComparison.OrdinalIgnoreCase);
                    });
                }
            }

        MatchDone:

            // Final fallback: no notes found for this split.
            var next = match ?? _emptyNode;
            if (!ReferenceEquals(currentNode, next))
                currentNode = next;

        }
    }
}

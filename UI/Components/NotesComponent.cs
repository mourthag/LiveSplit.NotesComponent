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
    class NotesComponent : IComponent
    {
        public NotesComponentSettings Settings { get; set; }
        public SplitNotes currentNode { get; protected set; }

        private SplitNotes _emptyNode;
        
        List<SplitNotes> loadedNotes = new List<SplitNotes>();

        public string ComponentName => "Notes";

        public IDictionary<string, Action> ContextMenuControls => null;

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

            checkCurrentSplit(state);
        }

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

            // Map sections without !!! to the split(s) after the last explicit !!! section
            string lastExplicit = null;
            int afterOffset = 0;
            foreach (var node in loadedNotes)
            {
                node.AfterSplitName = null;
                node.AfterSplitOffset = 0;

                if (!string.IsNullOrWhiteSpace(node.Name))
                {
                    lastExplicit = node.Name;
                    afterOffset = 0;
                    continue;
                }

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
                    match = loadedNotes.FirstOrDefault(x =>
                    {
                        if (string.IsNullOrWhiteSpace(x.AfterSplitName) || x.AfterSplitOffset <= 0)
                            return false;

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

            var next = match ?? _emptyNode;
            if (!ReferenceEquals(currentNode, next))
                currentNode = next;

        }
    }
}

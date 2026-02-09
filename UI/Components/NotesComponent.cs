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
            var match = loadedNotes.FirstOrDefault(x =>
                string.Equals(NormalizeSplitKey(x.Name), splitKey, StringComparison.OrdinalIgnoreCase));
            var next = match ?? _emptyNode;
            if (!ReferenceEquals(currentNode, next))
                currentNode = next;

        }
    }
}

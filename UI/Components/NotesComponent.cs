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
        
        List<SplitNotes> loadedNotes = new List<SplitNotes>();

        public string ComponentName => "Notes";

        public IDictionary<string, Action> ContextMenuControls => null;

        public NotesComponent(LiveSplitState state)
        {
            Settings = new NotesComponentSettings(state)
            {
                CurrentState = state
            };
            readFile();
        }

        public float HorizontalWidth => currentNode.HorizontalWidth;

        public float MinimumHeight => currentNode.MinimumHeight;

        public float MinimumWidth => currentNode.HorizontalWidth;

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

            if (state.CurrentSplit.Name != currentNode.Name)
                checkCurrentSplit(state);
        }

        public void readFile()
        {
            loadedNotes.Clear();

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
                        curSplitSection = lines.GetRange(curSplitStart.Value, curSplitEnd.Value - curSplitStart.Value - 1);

                        loadedNotes.Add(new SplitNotes(curSplitSection, Settings));

                        curSplitStart = null;
                        curSplitEnd = null;
                    }
                }

                index++;
            }
            Settings.FilePathChanged = false;
        }

        void checkCurrentSplit(LiveSplitState state)
        {
            foreach (SplitNotes curSplitNote in loadedNotes)
            {
                if (state.CurrentSplit.Name == curSplitNote.Name)
                {
                    currentNode = curSplitNote;
                }

            }

        }
    }
}

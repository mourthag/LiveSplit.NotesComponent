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

namespace LiveSplit.UI.Components
{
    class Note : IComponent
    {
        public NotesComponentSettings Settings { get; set; }

        public string ComponentName => "Note";

        public float HorizontalWidth => 200f;

        public float MinimumHeight { get; set; }

        public float VerticalHeight => 50f;

        public float MinimumWidth { get; set; }

        public float PaddingTop => 0f;

        public float PaddingBottom => 0f;

        public float PaddingLeft => 0f;

        public float PaddingRight => 0f;

        public IDictionary<string, Action> ContextMenuControls => null;

        Color bgColor;
        Color txtColor;
        NoteType Type;
        String Text;

        public Note(NoteType type, string note, NotesComponentSettings settings)
        {
            Settings = settings;
            Type = type;
            Text = note;

            switch (type)
            {
                case NoteType.LevelUp:
                    bgColor = Settings.LevelUpBgColor;
                    txtColor = Settings.LevelUpTxtColor;
                    break;
                case NoteType.ItemTracking:
                    bgColor = Settings.ItemTrackingBgColor;
                    txtColor = Settings.ItemTrackingTxtColor;
                    break;
                case NoteType.Teleport:
                    bgColor = Settings.TeleportBgColor;
                    txtColor = Settings.TeleportTxtColor;
                    break;
                case NoteType.Drop:
                    bgColor = Settings.DropBgColor;
                    txtColor = Settings.DropTxtColor;
                    break;
                case NoteType.Kill:
                    bgColor = Settings.KillBgColor;
                    txtColor = Settings.KillTxtColor;
                    break;
                case NoteType.Skip:
                    bgColor = Settings.SkipBgColor;
                    txtColor = Settings.SkipTxtColor;
                    break;
                case NoteType.Trophy:
                    bgColor = Settings.TrophyBgColor;
                    txtColor = Settings.TrophyTxtColor;
                    break;
                default:
                    bgColor = Settings.BackgroundColor;
                    txtColor = Color.White;
                    break;
            }
        }

        public void DrawHorizontal(Graphics g, LiveSplitState state, float height, Region clipRegion)
        {
            DrawBackgorund(g, HorizontalWidth, height);
        }

        public void DrawVertical(Graphics g, LiveSplitState state, float width, Region clipRegion)
        {
            DrawBackgorund(g, width, VerticalHeight);
        }

        void DrawBackgorund(Graphics g, float width, float height)
        {
            var bgBrush = new SolidBrush(bgColor);
            g.FillRectangle(bgBrush, 0, 0, width, height);
        }

        public Control GetSettingsControl(LayoutMode mode) => null;

        public XmlNode GetSettings(XmlDocument document)
        {
            throw new NotImplementedException();
        }

        public void SetSettings(XmlNode settings)
        {
            throw new NotImplementedException();
        }

        public void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode)
        {
        }

        public void Dispose()
        {
        }
    }
    public enum NoteType
    {
        LevelUp,
        ItemTracking,
        Teleport,
        Drop,
        Kill,
        Skip,
        Trophy,
        Title,
        Comment
    }
}

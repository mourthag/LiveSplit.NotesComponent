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

        public float HorizontalWidth { get; set; }

        public float MinimumHeight { get; set; }

        public float VerticalHeight { get; set; }

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

            VerticalHeight = 10f;
            HorizontalWidth = 100f;

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
            HorizontalWidth = g.MeasureString(Text, state.LayoutSettings.TextFont).Width;
            DrawBackgorund(g, HorizontalWidth, height);
            var textBrush = new SolidBrush(txtColor);
            g.DrawString(Text, state.LayoutSettings.TextFont, textBrush, new PointF(0, 0));
        }

        public void DrawVertical(Graphics g, LiveSplitState state, float width, Region clipRegion)
        {
            VerticalHeight = state.LayoutSettings.TextFont.GetHeight(g);
            MinimumWidth = g.MeasureString(Text, state.LayoutSettings.TextFont).Width;
            DrawBackgorund(g, width, VerticalHeight);
            var textBrush = new SolidBrush(txtColor);
            g.DrawString(Text, state.LayoutSettings.TextFont, textBrush, new PointF(0, 0));
        }

        void DrawBackgorund(Graphics g, float width, float height)
        {
            var bgBrush = new SolidBrush(bgColor);
            g.FillRectangle(bgBrush, 0, 0, width, height);
        }

        public Control GetSettingsControl(LayoutMode mode)
        {
            throw new NotSupportedException();
        }

        public XmlNode GetSettings(XmlDocument document)
        {
            throw new NotSupportedException();
        }

        public void SetSettings(XmlNode settings)
        {
            throw new NotSupportedException();
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

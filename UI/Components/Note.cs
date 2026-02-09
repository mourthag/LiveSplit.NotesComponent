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
    /// <summary>
    /// Renders a single note line inside the Notes component.
    /// A note has a type (which determines colors) and a text.
    /// </summary>
    class Note : IComponent
    {
        /// <summary>
        /// Settings instance shared with the parent component (colors, file path, etc.).
        /// </summary>
        public NotesComponentSettings Settings { get; set; }

        /// <summary>
        /// LiveSplit component display name.
        /// </summary>
        public string ComponentName => "Note";

        /// <summary>
        /// Desired width when the layout is horizontal.
        /// Computed from the rendered text width in <see cref="DrawHorizontal"/>.
        /// </summary>
        public float HorizontalWidth { get; set; }

        /// <summary>
        /// Minimum height the component needs.
        /// Not used by this note right now (the parent controls sizing).
        /// </summary>
        public float MinimumHeight { get; set; }

        /// <summary>
        /// Desired height when the layout is vertical.
        /// Computed from the current layout font in <see cref="DrawVertical"/>.
        /// </summary>
        public float VerticalHeight { get; set; }

        /// <summary>
        /// Desired minimum width (used by the LiveSplit layout engine).
        /// Computed from the rendered text width in <see cref="DrawVertical"/>.
        /// </summary>
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

        /// <summary>
        /// Creates a new note.
        /// </summary>
        /// <param name="type">Semantic type that controls colors.</param>
        /// <param name="note">The actual note text to render.</param>
        /// <param name="settings">Component settings (colors, etc.).</param>
        public Note(NoteType type, string note, NotesComponentSettings settings)
        {
            Settings = settings;
            Type = type;
            Text = note;

            VerticalHeight = 10f;
            HorizontalWidth = 50f;

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

        /// <summary>
        /// Draws the note in horizontal layout mode.
        /// LiveSplit passes <paramref name="height"/> as the available height; we compute our own width.
        /// </summary>
        /// <param name="g">Graphics surface used by LiveSplit.</param>
        /// <param name="state">LiveSplit state (font, etc.).</param>
        /// <param name="height">Available height in horizontal mode.</param>
        /// <param name="clipRegion">Clip region (unused).</param>
        public void DrawHorizontal(Graphics g, LiveSplitState state, float height, Region clipRegion)
        {
            // Measure text to inform the layout engine how wide this line wants to be.
            HorizontalWidth = g.MeasureString(Text, state.LayoutSettings.TextFont).Width;
            DrawBackgorund(g, HorizontalWidth, height);
            var textBrush = new SolidBrush(txtColor);
            g.DrawString(Text, state.LayoutSettings.TextFont, textBrush, new PointF(0, 0));
        }

        /// <summary>
        /// Draws the note in vertical layout mode.
        /// LiveSplit passes <paramref name="width"/> as the available width; we compute our own height.
        /// </summary>
        /// <param name="g">Graphics surface used by LiveSplit.</param>
        /// <param name="state">LiveSplit state (font, etc.).</param>
        /// <param name="width">Available width in vertical mode.</param>
        /// <param name="clipRegion">Clip region (unused).</param>
        public void DrawVertical(Graphics g, LiveSplitState state, float width, Region clipRegion)
        {
            // Height is font-driven; width is controlled by the parent but we still expose MinimumWidth.
            VerticalHeight = state.LayoutSettings.TextFont.GetHeight(g);
            MinimumWidth = g.MeasureString(Text, state.LayoutSettings.TextFont).Width;
            DrawBackgorund(g, width, VerticalHeight);
            var textBrush = new SolidBrush(txtColor);
            g.DrawString(Text, state.LayoutSettings.TextFont, textBrush, new PointF(0, 0));
        }

        /// <summary>
        /// Draws a solid background rectangle for the note.
        /// </summary>
        /// <param name="g">Graphics surface.</param>
        /// <param name="width">Rectangle width.</param>
        /// <param name="height">Rectangle height.</param>
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

    /// <summary>
    /// Note categories used to pick colors and (optionally) semantics.
    /// </summary>
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

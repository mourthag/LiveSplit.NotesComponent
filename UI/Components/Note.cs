using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using LiveSplit.Model;

namespace LiveSplit.UI.Components
{
    /// <summary>
    /// Renders a single note line inside the Notes component.
    /// A note has a type (which determines colors) and a text.
    /// </summary>
    internal class Note : IComponent
    {
        /// <summary>
        /// Maps each <see cref="NoteType"/> to a function that retrieves
        /// the corresponding (background, text) color pair from the settings.
        /// </summary>
        private static readonly Dictionary<NoteType, Func<NotesComponentSettings, (Color bg, Color txt)>> ColorMap
            = new Dictionary<NoteType, Func<NotesComponentSettings, (Color bg, Color txt)>>
        {
            [NoteType.LevelUp]      = s => (s.LevelUpBgColor, s.LevelUpTxtColor),
            [NoteType.ItemTracking] = s => (s.ItemTrackingBgColor, s.ItemTrackingTxtColor),
            [NoteType.Teleport]     = s => (s.TeleportBgColor, s.TeleportTxtColor),
            [NoteType.Drop]         = s => (s.DropBgColor, s.DropTxtColor),
            [NoteType.Kill]         = s => (s.KillBgColor, s.KillTxtColor),
            [NoteType.Skip]         = s => (s.SkipBgColor, s.SkipTxtColor),
            [NoteType.Trophy]       = s => (s.TrophyBgColor, s.TrophyTxtColor),
        };

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

        private readonly Color _bgColor;
        private readonly Color _txtColor;
        private readonly NoteType _type;
        private readonly string _text;

        /// <summary>
        /// Creates a new note.
        /// </summary>
        /// <param name="type">Semantic type that controls colors.</param>
        /// <param name="note">The actual note text to render.</param>
        /// <param name="settings">Component settings (colors, etc.).</param>
        public Note(NoteType type, string note, NotesComponentSettings settings)
        {
            Settings = settings;
            _type = type;
            _text = note;

            VerticalHeight = 10f;
            HorizontalWidth = 50f;

            if (ColorMap.TryGetValue(type, out var colorFn))
            {
                (_bgColor, _txtColor) = colorFn(Settings);
            }
            else
            {
                _bgColor = Settings.BackgroundColor;
                _txtColor = Color.White;
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
            HorizontalWidth = g.MeasureString(_text, state.LayoutSettings.TextFont).Width;
            DrawBackground(g, HorizontalWidth, height);
            using (var textBrush = new SolidBrush(_txtColor))
            {
                g.DrawString(_text, state.LayoutSettings.TextFont, textBrush, new PointF(0, 0));
            }
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
            MinimumWidth = g.MeasureString(_text, state.LayoutSettings.TextFont).Width;
            DrawBackground(g, width, VerticalHeight);
            using (var textBrush = new SolidBrush(_txtColor))
            {
                g.DrawString(_text, state.LayoutSettings.TextFont, textBrush, new PointF(0, 0));
            }
        }

        /// <summary>
        /// Draws a solid background rectangle for the note.
        /// </summary>
        /// <param name="g">Graphics surface.</param>
        /// <param name="width">Rectangle width.</param>
        /// <param name="height">Rectangle height.</param>
        private void DrawBackground(Graphics g, float width, float height)
        {
            using (var bgBrush = new SolidBrush(_bgColor))
            {
                g.FillRectangle(bgBrush, 0, 0, width, height);
            }
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
        Comment
    }
}

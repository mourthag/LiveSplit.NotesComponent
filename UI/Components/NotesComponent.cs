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
    class NotesComponent : IComponent
    {
        public NotesComponentSettings Settings { get; set; }
        public ComponentRendererComponent InternalComponent { get; protected set; }

        public string ComponentName => "Notes";

        public IDictionary<string, Action> ContextMenuControls => null;

        public NotesComponent(LiveSplitState state)
        {
            Settings = new NotesComponentSettings(state)
            {
                CurrentState = state
            };
            InternalComponent = new ComponentRendererComponent();
        }

        public float HorizontalWidth => InternalComponent.HorizontalWidth;

        public float MinimumHeight => InternalComponent.MinimumHeight;

        public float MinimumWidth => InternalComponent.HorizontalWidth;

        public float PaddingBottom => InternalComponent.PaddingBottom;

        public float PaddingLeft => InternalComponent.PaddingLeft;

        public float PaddingRight => InternalComponent.PaddingRight;

        public float PaddingTop => InternalComponent.PaddingTop;

        public float VerticalHeight => InternalComponent.VerticalHeight;

        public void Dispose()
        {
        }

        public void DrawHorizontal(Graphics g, LiveSplitState state, float height, Region clipRegion)
        {
        }

        public void DrawVertical(Graphics g, LiveSplitState state, float width, Region clipRegion)
        {
            
        }

        void DrawBackground(Graphics g, float width, float height)
        {
            var brush = new LinearGradientBrush(
                new PointF(0, 0),
                new PointF(width, height),
                Settings.BackgroundColor,
                Settings.BackgroundColor);
            g.FillRectangle(brush, 0, 0, width, height);
                );
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
        }

        public void readFile()
        {
            
        }
    }
}

using System;
using LiveSplit.Model;

namespace LiveSplit.UI.Components
{
    class NotesComponentFactory : IComponentFactory
    {
        public string ComponentName => "LiveNotes";

        public string Description => "Displays and highlights notes for Subsplits";

        public ComponentCategory Category => ComponentCategory.List;

        public IComponent Create(LiveSplitState state) => new NotesComponent(state);

        public string UpdateName => ComponentName;

        public string XMLURL => "";

        public string UpdateURL => "";

        public Version Version => Version.Parse("1.6");
    }
}

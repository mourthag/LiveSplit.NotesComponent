using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using LiveSplit.Model;

namespace LiveSplit.UI.Components
{
    class SplitNotes : ComponentRendererComponent
    {
        public string Name;
        List<Note> Notes = new List<Note>();
        NotesComponentSettings Settings;

        public SplitNotes(List<String> notes, NotesComponentSettings settings)
        {
            Settings = settings;
            Name = "";

            foreach (string curNote in notes)
            {
                if (!(curNote.Length <= 3))
                {
                    string strType = curNote.Substring(0, 3);
                    NoteType type = NoteType.Comment;
                    string note = curNote.Substring(0, curNote.Length);

                    switch (strType) {
                        case "!!!":
                            Name = curNote.Substring(3, curNote.Length - 3);
                            type = NoteType.Title;
                            break;
                        case ">>>":
                            type = NoteType.LevelUp;
                            break;
                        case "$$$":
                            type = NoteType.ItemTracking;
                            break;
                        case "<<<":
                            type = NoteType.Teleport;
                            break;
                        case "---":
                            type = NoteType.Drop;
                            break;
                        case "&&&":
                            type = NoteType.Kill;
                            break;
                        case "///":
                            type = NoteType.Skip;
                            break;
                        case "###":
                            type = NoteType.Trophy;
                            break;
                    }

                    if(type != NoteType.Comment)
                        note = curNote.Substring(3, curNote.Length - 3);

                    Notes.Add(new Note(type, note, Settings));
                }
            }

            VisibleComponents = Notes;
        }
    }

}

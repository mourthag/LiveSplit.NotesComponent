using System;
using System.Collections.Generic;
using System.Drawing;
using LiveSplit.Model;

namespace LiveSplit.UI.Components
{
    /// <summary>
    /// Represents all notes for a single split section.
    /// Parses raw text lines into typed <see cref="Note"/> objects.
    /// </summary>
    internal class SplitNotes : ComponentRendererComponent
    {
        private const int EmptyNoteLineCount = 1;

        /// <summary>
        /// Maps 3-character line prefixes to their corresponding <see cref="NoteType"/>.
        /// </summary>
        private static readonly Dictionary<string, NoteType> MarkerMap
            = new Dictionary<string, NoteType>
        {
            [">>>"] = NoteType.LevelUp,
            ["$$$"] = NoteType.ItemTracking,
            ["<<<"] = NoteType.Teleport,
            ["---"] = NoteType.Drop,
            ["&&&"] = NoteType.Kill,
            ["///"] = NoteType.Skip,
            ["###"] = NoteType.Trophy,
        };

        /// <summary>
        /// The split name extracted from a "!!!SplitName" header line (empty if unnamed).
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// For unnamed sections that follow a named section: the name of the preceding named split.
        /// </summary>
        public string AfterSplitName { get; set; }

        /// <summary>
        /// How many unnamed sections after the anchor split this section is (1-based).
        /// </summary>
        public int AfterSplitOffset { get; set; }

        /// <summary>
        /// For leading unnamed sections (before the first !!!): the direct split index to map to.
        /// </summary>
        public int? TargetSplitIndex { get; set; }

        private readonly List<Note> _notes = new List<Note>();
        private readonly NotesComponentSettings _settings;

        /// <summary>
        /// Creates a new <see cref="SplitNotes"/> by parsing the given raw text lines.
        /// </summary>
        /// <param name="notes">Raw lines from the notes file for this section.</param>
        /// <param name="settings">Shared component settings (colors, etc.).</param>
        public SplitNotes(List<string> notes, NotesComponentSettings settings)
        {
            _settings = settings;
            Name = "";

            foreach (string curNote in notes)
            {
                if (string.IsNullOrWhiteSpace(curNote))
                    continue;

                // Lines with 3 or fewer characters can still be valid short notes (e.g. "Hi").
                // Only attempt marker parsing when the line is long enough.
                if (curNote.Length > 3)
                {
                    string marker = curNote.Substring(0, 3);

                    // Special markers that don't produce a visible note
                    if (marker == "!!!")
                    {
                        Name = curNote.Substring(3).Trim();
                        continue; // Split-Header nur zur Zuordnung, nicht als Note rendern
                    }

                    if (marker == "%%%")
                    {
                        continue; // Kommentarzeile im Notes-File komplett ignorieren
                    }

                    // Typed note markers
                    if (MarkerMap.TryGetValue(marker, out NoteType mappedType))
                    {
                        string text = curNote.Substring(3).Trim();
                        _notes.Add(new Note(mappedType, text, _settings));
                        continue;
                    }
                }

                // No marker matched — treat as a plain comment note.
                _notes.Add(new Note(NoteType.Comment, curNote.Trim(), _settings));
            }

            if (_notes.Count == 0)
            {
                for (int i = 0; i < EmptyNoteLineCount; i++)
                    _notes.Add(new Note(NoteType.Comment, "", _settings));
            }

            VisibleComponents = _notes;
        }
    }
}

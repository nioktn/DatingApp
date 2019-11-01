using DatingApp.API.Core;
using DatingApp.API.Models;

namespace DatingApp.API.BLL
{
    public class NotesManager : INotesManager
    {
        INoteRepository _noteRepository;

        public NotesManager(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public Note TranslateNote(Note note)
        {
            var currNote = new Note
            {
                Text = note.Text.ToUpper(), 
                Title = note.Title + "_translated", 
                User = note.User
            };
            return currNote;
        }
    }
}
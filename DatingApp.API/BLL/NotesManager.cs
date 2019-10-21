using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            var currNote = new Note();
            currNote.Text = note.Text.ToUpper();
            currNote.Title = note.Title + "_translated";
            currNote.User = note.User;
            return currNote;
        }
    }
}
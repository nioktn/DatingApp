using System.Collections.Generic;
using DatingApp.API.Models;

namespace DatingApp.API.BLL
{
    public class NotesManager : INotesManager
    {
        public ICollection<Note> GetUserNotes(int userId)
        {
            throw new System.NotImplementedException();
        }
    }
}
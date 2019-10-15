using System.Collections.Generic;
using DatingApp.API.Models;

namespace DatingApp.API.BLL
{
    public interface INotesManager
    {
        ICollection<Note> GetUserNotes(int userId);
    }
}
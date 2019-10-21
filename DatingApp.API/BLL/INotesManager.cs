using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.API.Models;

namespace DatingApp.API.BLL
{
    public interface INotesManager
    {
        Note TranslateNote(Note note);
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.API.Models;

namespace DatingApp.API.Core
{
    public interface INoteRepository
    {
        Task<Note> GetNote(int id);
        Task Add(Note note);
        void Update(Note note);
        void Remove(Note note);
        Task<ICollection<Note>> GetAllNotesOfUser(int id); // it is one of Litvinov's requirement
    }
}
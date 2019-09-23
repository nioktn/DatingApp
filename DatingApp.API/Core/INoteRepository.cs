using System.Threading.Tasks;
using DatingApp.API.Models;

namespace DatingApp.API.Core
{
    public interface INoteRepository
    {
        Task<Note> GetNote(int id);
        Task Add(Note note);
        void Remove(Note note);
    }
}
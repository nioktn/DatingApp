using System.Collections.ObjectModel;
using System.Threading.Tasks;
using DatingApp.API.Core;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Persistence
{
    public class NoteRepository : INoteRepository
    {
        private readonly DataContext _context;

        public NoteRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Note> GetNote(int id)
        {
//            return await _context.Notes.FindAsync(id);
            return await _context.Notes.FirstOrDefaultAsync(_ => _.Id == id);
        }

        public async Task Add(Note note)
        {
            await _context.Notes.AddAsync(note);
        }

        public void Remove(Note note)
        {
            _context.Notes.Remove(note); 
        }
    }
}
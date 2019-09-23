using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base (options) { }
                
        public DbSet<Note> Notes { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
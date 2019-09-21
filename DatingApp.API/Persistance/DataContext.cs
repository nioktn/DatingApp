using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Persistance
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base (options) { }
                
        public DbSet<Value> Values { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
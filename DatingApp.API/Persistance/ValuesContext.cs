using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Persistance
{
    public class ValuesContext : DbContext
    {
        public ValuesContext(DbContextOptions options) : base (options) { }
                
        public DbSet<Value> Values { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
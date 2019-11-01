using System.Collections.ObjectModel;
using System.Threading.Tasks;
using DatingApp.API.Core;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

namespace DatingApp.API.Persistence
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public Task<User> GetUser(int id)
        {
            return _context.Users.FirstOrDefaultAsync(_ => _.Id == id);
        }

        public Task Add(User user)
        {
            throw new System.NotImplementedException();
        }

        public void Update(User user)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(User user)
        {
            _context.Users.Remove(user);
            
        }
    }
}
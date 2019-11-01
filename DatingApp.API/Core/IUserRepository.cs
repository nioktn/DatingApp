using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.API.Models;

namespace DatingApp.API.Core
{
    public interface IUserRepository
    {
        Task<User> GetUser(int id);
        Task Add(User user);
        void Update(User user);
        void Remove(User user);
    }
}
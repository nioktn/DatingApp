using System.Threading.Tasks;
using DatingApp.API.Models;

namespace DatingApp.API.Core
{
    public interface IAuthRepository
    {
         Task<User> Register(User user, string password);
         Task<User> Login(string username, string password);
         Task<bool> UserExists(string username);
         Task<User> GetUserById(int id);// it can be useful in the future
    }
}
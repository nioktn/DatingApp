using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Controllers.Resources
{
    public class UserLoginResource
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
using System.Collections.Generic;
using DatingApp.API.Models;

namespace DatingApp.API.Controllers.Resources
{
    public class UserResource
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public ICollection<Note> Notes { get; set; }
    }
}
using System;
using DatingApp.API.Models;

namespace DatingApp.API.Controllers.Resources
{
    public class SaveNoteResource
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
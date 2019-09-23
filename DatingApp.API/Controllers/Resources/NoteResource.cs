using System;

namespace DatingApp.API.Controllers.Resources
{
    public class NoteResource
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
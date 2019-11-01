using System;
using System.Collections.Generic;
using System.Text;

namespace DatingApp.Tests.Model
{
    public class NoteModel
    {
        public int id { get; set; }
        public string title { get; set; }
        public string text { get; set; }
        public int userId { get; set; }
        public DateTime createdDate { get; set; }
    }
}

using ElearningDatabase.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Elearning.Database.Models
{
    public class Author
    {
        public Course Course { get; set; }
        public int CourseId { get; set; }

        public int Id { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}

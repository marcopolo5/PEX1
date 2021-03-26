using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EFElearning.Models
{
    public class Quiz
    {
        public Course Course { get; set; }
        public int CourseId { get; set; }
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<Question> Questions { get; set; }
    }
}
using ElearningDatabase.Validation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ElearningDatabase.Models
{
    public class Lesson
    {
        public Course Course { get; set; }

        [Required]
        public int CourseId { get; set; }

        public int Id { get; set; }

        [Required]
        [MaxLength(ValidationRules.courseMaxLength)]
        public string Name { get; set; }

        [Required]
        public string Content { get; set; }

        public List<Resource> Resources { get; set; }


    }
}
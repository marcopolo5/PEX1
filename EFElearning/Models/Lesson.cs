using EFElearning.Validation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EFElearning.Models
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

        public List<Resource> Resources { get; set; }
    }
}
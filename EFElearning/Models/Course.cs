using EFElearning.Validation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EFElearning.Models
{
    public enum DifficultyEnum
    {
        Beginner,
        Intermediate,
        Advanced,
    }

    public class Course
    {
        [Required]
        [MaxLength(ValidationRules.courseMinLength)]
        public string Category { get; set; }

        [Required]
        [MaxLength(ValidationRules.courseMaxLength)]
        public string Description { get; set; }

        [Required]
        [MaxLength(ValidationRules.courseMaxLength)]
        public DifficultyEnum Difficulty { get; set; }

        public List<EnrolledUserCourse> EnrolledUserCourses { get; set; }
        public int Id { get; set; }
        public List<Lesson> Lessons { get; set; }

        [Required]
        [MaxLength(ValidationRules.courseMaxLength)]
        public string Name { get; set; }

        public List<Quiz> Quizes { get; set; }
    }
}
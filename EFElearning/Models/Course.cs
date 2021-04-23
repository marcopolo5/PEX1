using Elearning.Database.Models;
using ElearningDatabase.Validation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ElearningDatabase.Models
{
    public enum DifficultyEnum
    {
        Beginner,
        Intermediate,
        Advanced,
    }

    public enum CategoryEnum
    {
        IT,
        Psychology,
        Education,
        Medicine,
        Law,
        Economics,
        Finance,
        BussinesManagement,
        Engineering,
        Health,
        Lifestyle,
        Arts,
        Literature

    }

    public class Course
    {
        [Required]
        [MaxLength(ValidationRules.courseMinLength)]
        public CategoryEnum Category { get; set; }

        [Required]
        [MaxLength(ValidationRules.courseMaxLength)]
        public string Description { get; set; }

        [Required]
        [MaxLength(ValidationRules.courseMaxLength)]
        public DifficultyEnum Difficulty { get; set; }

        [Required]
        public Author Author { get; set; }
        public int Id { get; set; }
        public List<Lesson> Lessons { get; set; }

        [Required]
        [MaxLength(ValidationRules.courseMaxLength)]
        public string Name { get; set; }

        public List<Quiz> Quizes { get; set; }

        public List<Review> Reviews { get; set; }

    }
}
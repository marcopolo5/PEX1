using System.ComponentModel.DataAnnotations;

namespace Elearning.Database.Models
{
    public class Author
    {
        public int Id { get; set; }
        [Required]
        //[MaxLength(ValidationRules.courseMinLength)]
        public int UserId { get; set; }
        [Required]
        public int CourseId { get; set; }
    }
}
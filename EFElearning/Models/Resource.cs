using System.ComponentModel.DataAnnotations;

namespace EFElearning.Models
{
    public class Resource
    {
        [Required]
        public string File { get; set; }
        public int Id { get; set; }
        public Lesson Lesson { get; set; }
        public int LessonId { get; set; }
        public int Type { get; set; }
    }
}
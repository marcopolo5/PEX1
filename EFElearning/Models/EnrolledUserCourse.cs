using System.ComponentModel.DataAnnotations;

namespace ElearningDatabase.Models
{
    public class EnrolledUserCourse
    {
        public Course Course { get; set; }

        [Required]
        public int CourseId { get; set; }

        public int Id { get; set; }
        public User User { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}
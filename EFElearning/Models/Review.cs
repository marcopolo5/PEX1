using ElearningDatabase.Models;
using System.ComponentModel.DataAnnotations;

namespace Elearning.Database.Models
{
    public class Review
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int CourseId { get; set; }
    }
}
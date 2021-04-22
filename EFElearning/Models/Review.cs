using ElearningDatabase.Models;
using System.Collections.ObjectModel;
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

        public Review(int userId, int courseId, string content)
        {
            UserId = userId;
            CourseId = courseId;
            Content = content;
        }

    }
}
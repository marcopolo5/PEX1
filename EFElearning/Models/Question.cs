using System.ComponentModel.DataAnnotations;

namespace EFElearning.Models
{
    public class Question
    {
        public int Id { get; set; }

        [Required]
        public string QuestionText { get; set; }

        [Required]
        public int CorrectAnswer { get; set; }

        [Required]
        public string Answer1 { get; set; }

        [Required]
        public string Answer2 { get; set; }

        [Required]
        public string Answer3 { get; set; }

        [Required]
        public string Answer4 { get; set; }
    }
}
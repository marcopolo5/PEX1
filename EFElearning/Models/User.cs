using Elearning.Database.Models;
using ElearningDatabase.Validation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ElearningDatabase.Models
{
    public enum RoleEnum
    {
        Admin,
        Trainer,
        Student
    }

    public class User
    {
        [Required]
        [MaxLength(ValidationRules.userMaxLength)]
        public string Email { get; set; }

        [Required]
        [MaxLength(ValidationRules.userMaxLength)]
        public string FirstName { get; set; }

        public int Id { get; set; }

        [Required]
        [MaxLength(ValidationRules.userMaxLength)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(ValidationRules.userMinLength)]
        public string Password { get; set; }

        public List<UserProgress> Progresses { get; set; }

        [Required]
        [MaxLength(ValidationRules.userMinLength)]
        public RoleEnum Role { get; set; }

        [Required]
        [MaxLength(ValidationRules.userMinLength)]
        public string Username { get; set; }
    }
}
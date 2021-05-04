using System.ComponentModel.DataAnnotations;

namespace ElearningDatabase.Models
{
    public class Resource
    {
        [Required]
        public string File { get; set; }
        public int Id { get; set; }

    }
}
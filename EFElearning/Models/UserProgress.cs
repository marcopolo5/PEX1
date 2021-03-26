namespace EFElearning.Models
{
    public class UserProgress
    {
        public Course Course { get; set; }
        public int CourseId { get; set; }
        public int Id { get; set; }
        public int Progress { get; set; }

        public User User { get; set; }

        public int UserId { get; set; }
    }
}
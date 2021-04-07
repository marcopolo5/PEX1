using System.Collections.Generic;

namespace Elearning.Dtos
{
    public class QuizDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<QuestionDto> Questions { get; set; }
    }
}
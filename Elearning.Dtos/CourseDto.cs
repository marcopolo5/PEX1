using System;
using System.Collections.Generic;
using System.Text;

namespace Elearning.Dtos
{
    public class CourseDto
    {
        public string Category { get; set; }
        public string Description { get; set; }
        public string Difficulty { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }

        public List<QuizDto> Quizes { get; set; }
    }
}

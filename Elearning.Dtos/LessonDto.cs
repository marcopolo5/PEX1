using System;
using System.Collections.Generic;

namespace Elearning.Dtos
{
    public class LessonDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<ResourceDto> Resources { get; set; }
    }
}

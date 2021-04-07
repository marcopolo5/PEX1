using ElearningDatabase;
using ElearningDatabase.Models;
using System.Collections.Generic;
using System.Linq;

namespace Elearning.Business
{
    public class CourseService
    {
        public List<Lesson> GetLessons(int courseId)
        {
            using (ElearningContext elearningContext = new ElearningContext())
            {
                var lessons = elearningContext.Courses.Where(x => x.Id == courseId).Select(x => x.Lessons).ToList().FirstOrDefault();
                return lessons;
            }
        }
    }
}
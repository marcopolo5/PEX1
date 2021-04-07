using ElearningDatabase;
using ElearningDatabase.Models;
using System.Collections.Generic;
using System.Linq;

namespace Elearning.Business
{
    public class LessonService
    {
        public List<Resource> GetLessons(int lessonId)
        {
            using (ElearningContext elearningContext = new ElearningContext())
            {
                var resources = elearningContext.Lessons.Where(x => x.Id == lessonId).Select(x => x.Resources).ToList().FirstOrDefault();
                return resources;
            }
        }
    }
}
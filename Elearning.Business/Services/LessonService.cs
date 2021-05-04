using ElearningDatabase;
using ElearningDatabase.Models;
using System.Collections.Generic;
using System.Linq;

namespace Elearning.Business
{
    public class LessonService
    {
        public string GetContent(int lessonId)
        {
            using (ElearningContext elearningContext = new ElearningContext())
            {
                var resources = elearningContext.Lessons.Where(x => x.Id == lessonId).Select(x => x.Content).ToList().FirstOrDefault();
                return resources;
            }
        }

        public string GetResourceFile(int resourceId)
        {
            using (ElearningContext elearningContext = new ElearningContext())
            {
                var resource = elearningContext.Resources.Where(x => x.Id == resourceId).Select(x=>x.File).FirstOrDefault();
                return resource;
            }
        }
    }
}
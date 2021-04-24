using Elearning.Database.Models;
using ElearningDatabase;
using ElearningDatabase.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public List<Review> GetReviews(int courseId)
        {
            using (ElearningContext elearningContext = new ElearningContext())
            {
                var reviews = elearningContext.Courses.Where(x => x.Id == courseId).Select(x => x.Reviews).ToList().FirstOrDefault();
                return reviews;
            }
        }

        public List<Resource> GetResources(int lessonId)
        {
            using (ElearningContext elearningContext = new ElearningContext())
            {
                var resources = elearningContext.Lessons.Where(x => x.Id == lessonId).Select(x => x.Resources).ToList().FirstOrDefault();
                return resources;
            }
        }

        public void InsertReview(User user, int courseId, string review)
        {
            if(review!=" " )
            {
                using (ElearningContext elearningContext = new ElearningContext())
                {
                    elearningContext.Reviews.Add(new Review(user.Id, courseId, review)
                    {
                        Content = review,
                        UserId= user.Id,
                        CourseId= courseId
                    });
                    elearningContext.SaveChanges();
                }
            }

        }

        public void InsertLesson(Lesson lesson)
        {
            using (ElearningContext elearningContext = new ElearningContext())
            {
                elearningContext.Lessons.Add(new Lesson()
                {
                    Name= lesson.Name,
                    CourseId= lesson.CourseId,
                    Content=lesson.Content,
                    Resources= lesson.Resources
                });
                elearningContext.SaveChanges();
            }
        }

    }
}
using Elearning.Database.Models;
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

        public List<Quiz> GetQuizzes(int courseId)
        {
            using (ElearningContext elearningContext = new ElearningContext())
            {
                var quizzes = elearningContext.Courses.Where(x => x.Id == courseId).Select(x => x.Quizes).ToList().FirstOrDefault();
                return quizzes;
            }
        }

        public List<Question> GetQuestions(int quizId)
        {
            using (ElearningContext elearningContext = new ElearningContext())
            {
                var question = elearningContext.Questions.Where(x => x.QuizId == quizId);
                return question.ToList();
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

        public Resource GetResources(int lessonId)
        {
            using (ElearningContext elearningContext = new ElearningContext())
            {
                var resources = elearningContext.Lessons.Where(x => x.Id == lessonId).Select(x => x.Resource).ToList().FirstOrDefault();
                return resources;
            }
        }

        public List<Quiz> GetQuizes(int courseId)
        {
            using (ElearningContext elearningContext = new ElearningContext())
            {
                var quiz = elearningContext.Quizes.Where(x => x.CourseId == courseId);
                return quiz.ToList();
            }
        }

        public void InsertQuestion(int quizId, Question question)
        {
            if (question != null)
            {
                using (ElearningContext elearningContext = new ElearningContext())
                {
                    elearningContext.Questions.Add(new Question()
                    {
                        QuestionText= question.QuestionText,
                        Answer1=question.Answer1,
                        Answer2 = question.Answer2,
                        Answer3 = question.Answer3,
                        Answer4 = question.Answer4,
                        CorrectAnswer= question.CorrectAnswer,
                        QuizId= quizId
                    });
                    elearningContext.SaveChanges();
                }
            }
        }

        public Quiz InsertQuiz(Quiz quiz)
        {
            using (ElearningContext elearningContext = new ElearningContext())
                {
                    elearningContext.Quizes.Add(new Quiz()
                    {
                        Name= quiz.Name,
                        Questions= quiz.Questions,
                        CourseId=  quiz.CourseId
                        
                    });
                    elearningContext.SaveChanges();
                     var newId = elearningContext.Quizes.Max(x => x.Id);
                    var returnedQuiz = elearningContext.Quizes.Where(x => x.Id == newId).ToList().FirstOrDefault();
                    return returnedQuiz;
            }
            
        }

        public void InsertReview(User user, int courseId, string review)
        {
            if (review != " ")
            {
                using (ElearningContext elearningContext = new ElearningContext())
                {
                    elearningContext.Reviews.Add(new Review(user.Id, courseId, review)
                    {
                        Content = review,
                        UserId = user.Id,
                        CourseId = courseId
                    });
                    elearningContext.SaveChanges();
                }
            }
        }

        public void InsertLesson(Lesson lesson, int courseId)
        {
            using (ElearningContext elearningContext = new ElearningContext())
            {
                elearningContext.Lessons.Add(new Lesson()
                {
                    Name = lesson.Name,
                    Content = lesson.Content,
                    Resource = lesson.Resource,
                    CourseId = courseId
                });
                elearningContext.SaveChanges();
            }
        }

        public Course InsertCourse(Course course, User user)
        {
            if (course != null)
            {
                using (ElearningContext elearningContext = new ElearningContext())
                {
                    elearningContext.Courses.Add(new Course()
                    {
                        Name = course.Name,
                        Category = course.Category,
                        Difficulty = course.Difficulty,
                        Description = course.Description
                    });
                    elearningContext.SaveChanges();
                    var newId = elearningContext.Courses.Max(x => x.Id);
                    elearningContext.Authors.Add(new Author { UserId = user.Id, CourseId = newId });
                    elearningContext.SaveChanges();
                    var returnedCourse = elearningContext.Courses.Where(x => x.Id == newId).ToList().FirstOrDefault();
                    return returnedCourse;
                }
            }
            return null;
        }

        public void RemoveLesson(Lesson lesson)
        {
            using (ElearningContext elearningContext = new ElearningContext())
            {
                elearningContext.Lessons.Remove(lesson);
                elearningContext.SaveChanges();
            }
        }

        public void RemoveQuestion(Question question)
        {
            using (ElearningContext elearningContext = new ElearningContext())
            {
                elearningContext.Questions.Remove(question);
                elearningContext.SaveChanges();
            }
        }

        public void UpdateCourse(Course course)
        {
            using (ElearningContext elearningContext = new ElearningContext())
            {
                elearningContext.Courses.Update(course);
                elearningContext.SaveChanges();
            }
        }
    }
}
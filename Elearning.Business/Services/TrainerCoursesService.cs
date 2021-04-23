//using ElearningDatabase;
//using ElearningDatabase.Models;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Linq;

//namespace Elearning.Business.Services
//{
//    public class TrainerCoursesService
//    {
//        public List<Course> GetAllCoursesOfATrainer(User trainer)
//        {
//            using (ElearningContext context = new ElearningContext())
//            {
//                var usersCourses = (from course in context.Courses
//                                    where course.UserId == trainer.Id
//                                    select new Course()
//                                    {
//                                        Id = course.Id,
//                                        Name = course.Name,
//                                        Description = course.Description,
//                                        Category = course.Category,
//                                        Difficulty = course.Difficulty
//                                    }).ToList();
//                return usersCourses;
//            }
//        }

//        public List<Course> GetSuggestedCoursesForAnUser(User author)
//        {
//            using (ElearningContext context = new ElearningContext())
//            {
//                var usersCourses = from course in context.Courses
//                                   where course.UserId == author.Id
//                                   select new Course()
//                                   {
//                                       Id = course.Id,
//                                       Name = course.Name,
//                                       Description = course.Description,
//                                       Category = course.Category,
//                                       Difficulty = course.Difficulty
//                                   };
//                var allCourses = from course in context.Courses
//                                 select new Course()
//                                 {
//                                     Id = course.Id,
//                                     Name = course.Name,
//                                     Description = course.Description,
//                                     Category = course.Category,
//                                     Difficulty = course.Difficulty
//                                 };

//                var suggestedCourses = allCourses.Except(usersCourses).ToList();
//                return suggestedCourses;

//            }
//        }
//    }
//}

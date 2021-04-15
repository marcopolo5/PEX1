using ElearningDatabase;
using ElearningDatabase.Models;
using System.Linq;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;

namespace Elearning.Business
{
    public class CourseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public DifficultyEnum Difficulty { get; set; }



    }
    public class AllCoursesBusiness
    {
        public List<CourseDTO> GetAllCoursesOfAnUser(User user)
        {
            using (ElearningContext context = new ElearningContext())
            {
                var usersCourses = (from userProgress in context.UserProgresses
                                    where userProgress.UserId == user.Id
                                    select new CourseDTO()
                                    {
                                        Id = userProgress.Course.Id,
                                        Name = userProgress.Course.Name,
                                        Description = userProgress.Course.Description,
                                        Category = userProgress.Course.Category,
                                        Difficulty = userProgress.Course.Difficulty
                                    }).ToList();
                    return usersCourses;
            }
        }

        public List<CourseDTO> GetSuggestedCoursesForAnUser(User user)
        {
            using (ElearningContext context = new ElearningContext())
            {
                var usersCourses = from userProgress in context.UserProgresses
                                   where userProgress.UserId == user.Id
                                   select new CourseDTO()
                                   {
                                       Id = userProgress.Course.Id,
                                       Name = userProgress.Course.Name,
                                       Description = userProgress.Course.Description,
                                       Category = userProgress.Course.Category,
                                       Difficulty = userProgress.Course.Difficulty
                                   };
                var allCourses = from course in context.Courses
                                 select new CourseDTO()
                                 {
                                     Id = course.Id,
                                     Name = course.Name,
                                     Description = course.Description,
                                     Category = course.Category,
                                     Difficulty = course.Difficulty
                                 };

                var suggestedCourses = allCourses.Except(usersCourses).ToList();
                return suggestedCourses;

            }
        }
    }
}

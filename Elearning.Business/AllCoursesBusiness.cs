using ElearningDatabase;
using ElearningDatabase.Models;
using System.Linq;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;

namespace Elearning.Business
{
    public class AllCoursesBusiness
    {
        public List<Course> GetAllCoursesOfAnUser(User user)
        {
            using (ElearningContext context = new ElearningContext())
            {
                //try
                //{
                    var usersCourses = context.UserProgresses
                                      .Where(enrollment => enrollment.UserId == user.Id)
                                      .Select(enrollment => enrollment.Course)
                    .ToList();
                    return usersCourses;
                //}
                //catch (Exception exception)
                //{
                //    return new List<Course>();
                //}
                //TODO: handling exceptions?
            }
        }

        public List<Course> GetSuggestedCoursesForAnUser(User user)
        {
            using (ElearningContext context = new ElearningContext())
            {
                //try
                //{
                    var usersCourses = context.UserProgresses
                                      .Where(enrollment => enrollment.UserId == user.Id)
                                      .Select(enrollment => enrollment.Course);
                    var allCourses = context.Courses
                                        .Select(course => course);

                    var suggestedCourses = allCourses.Except(usersCourses).ToList();
                    return suggestedCourses;
                //}
                //catch (Exception exception)
                //{
                //    return new List<Course>();
                //}

            }
        }
    }
}

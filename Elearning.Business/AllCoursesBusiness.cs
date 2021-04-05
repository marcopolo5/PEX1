using ElearningDatabase;
using ElearningDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elearning.Business
{
    public class AllCoursesBusiness
    {
        public List<Course> GetAllCoursesOfAnUser(User user)
        {
            ElearningContext context = new ElearningContext();
            var usersCourses = context.UserProgresses
                              .Where(enrollment => enrollment.UserId == user.Id)
                              .Select(enrollment => enrollment.Course)
                              .Cast<Course>()
                              .ToList();
            return usersCourses;
        }
    }
}

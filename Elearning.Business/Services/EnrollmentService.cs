using ElearningDatabase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Elearning.Business.Services
{
    public class EnrollmentService
    {
        public void AddEnrollment(int userId, int courseId)
        {
            try
            {
                using (ElearningContext elearningContext = new ElearningContext())
                {
                    elearningContext.UserProgresses.Add(new ElearningDatabase.Models.UserProgress()
                    {
                        UserId = userId,
                        CourseId = courseId,
                        Progress = 0
                    });
                    elearningContext.SaveChanges();
                }

            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}

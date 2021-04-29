﻿using ElearningDatabase;
using ElearningDatabase.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Elearning.Business.Services
{
    public class TrainerCoursesService
    {
        public List<Course> GetAllCoursesOfATrainer(User trainer)
        {
            using (ElearningContext context = new ElearningContext())
            {
                var usersCourses = (from authorCourse in context.Authors
                                    where authorCourse.UserId == trainer.Id
                                    select new Course()
                                    {
                                        Id = authorCourse.Course.Id,
                                        Name = authorCourse.Course.Name,
                                        Description = authorCourse.Course.Description,
                                        //Category = authorCourse.Course.Category,
                                        Difficulty = authorCourse.Course.Difficulty
                                    }).ToList();
                //var usersCourses = context.Authors.Where(x => x.UserId == trainer.Id).Select(x => x.Course).ToList();
                return usersCourses;
            }
        }

        public List<Course> GetSuggestedCoursesForAnUser(User author)
        {
            using (ElearningContext context = new ElearningContext())
            {
                var usersCourses = from authorCourse in context.Authors
                                   where authorCourse.UserId == author.Id
                                   select new Course()
                                   {
                                       Id = authorCourse.Course.Id,
                                       Name = authorCourse.Course.Name,
                                       Description = authorCourse.Course.Description,
                                       //Category = authorCourse.Course.Category,
                                       Difficulty = authorCourse.Course.Difficulty
                                   };
                var allCourses = from course in context.Courses
                                 select new Course()
                                 {
                                     Id = course.Id,
                                     Name = course.Name,
                                     Description = course.Description,
                                     //Category = course.Category,
                                     Difficulty = course.Difficulty
                                 };

                var suggestedCourses = allCourses.Except(usersCourses).ToList();
                return suggestedCourses;

            }
        }

        public void DeleteCourse(Course course)
        {
            using (ElearningContext context = new ElearningContext())
            {
                Course deletedCourse = context.Courses.Where(x => x.Id == course.Id).ToList().FirstOrDefault();
                context.Courses.Remove(deletedCourse);
                context.SaveChanges();
            }
        }

        public void UpdateCourse(Course course)
        {
            using (ElearningContext context = new ElearningContext())
            {
                Course updatedCourse = context.Courses.Where(x => x.Id == course.Id).ToList().FirstOrDefault();
                updatedCourse.Name = course.Name;
                updatedCourse.Description = course.Description;
                updatedCourse.Difficulty = course.Difficulty;
                updatedCourse.Category = course.Category;
                context.SaveChanges();
            }
        }
    }
}

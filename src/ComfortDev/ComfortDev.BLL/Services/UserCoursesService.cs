using ComfortDev.BLL.Interfaces;
using ComfortDev.Common.Entities;
using ComfortDev.DAL.UnitsOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComfortDev.BLL.Services
{
    public class UserCoursesService : IUserCoursesService
    {
        private readonly ComfortDevUnitOfWork database;
        public UserCoursesService(ComfortDevUnitOfWork uow)
        {
            database = uow;
        }
        public UserCoursesService() : this(new ComfortDevUnitOfWork()) { }

        public UserCourse CreateCourse(int userId, int topicId, int nTasks, int durationDays)
        {
            if (database.UserCourses.Find(c => c.UserId == userId && c.EndDate > DateTime.Now).Count() > 0)
            {
                throw new Exception("User has unfinished course.");
            }

            var newUserCourse = new UserCourse
            {
                UserId = userId,
                EndDate = DateTime.Now.AddDays(durationDays)
            };

            var tasksForTopic = database.Tasks.Find(t => t.TopicId == topicId).ToList();
            var rand = new Random();
            for (int i = 0; i < nTasks; ++i)
            {
                var randomTask = tasksForTopic[rand.Next(0, tasksForTopic.Count)];
                tasksForTopic.Remove(randomTask);

                var newCourseTask = new CourseTask
                {
                    TaskId = randomTask.Id,
                    CompPer = 0
                };
                newUserCourse.CourseTasks.Add(newCourseTask);
            }

            database.UserCourses.Create(newUserCourse);
            database.Save();

            return newUserCourse;
        }

        public UserCourse GetActiveCourse(int userId)
        {
            return database.UserCourses
                .Find(course => course.UserId == userId && course.EndDate > DateTime.Now)
                .FirstOrDefault();
        }

        public void SetTaskCompletionPercent(int courseTaskId, int completionPercent)
        {
            var courseTask = database.CourseTasks.Get(courseTaskId);
            if (courseTask != null)
            {
                courseTask.CompPer = completionPercent;
                database.CourseTasks.Update(courseTask);
                database.Save();
            }
            else
            {
                throw new ArgumentException($"CourseTask with id: {courseTaskId} is not exist.");
            }
        }
    }
}

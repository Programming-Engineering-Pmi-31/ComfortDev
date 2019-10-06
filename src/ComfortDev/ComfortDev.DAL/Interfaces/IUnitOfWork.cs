using System;
using System.Collections.Generic;
using System.Text;
using ComfortDev.Common.Entities;

namespace ComfortDev.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<CourseTask> CourseTasks { get; }
        IRepository<Task> Tasks { get; }
        IRepository<TestAnswer> TestAnswers { get; }
        IRepository<TestQuestion> TestQuestions { get; }
        IRepository<Topic> Topics { get; }
        IRepository<User> Users { get; }
        IRepository<UserCourse> UserCourses { get; }
        void Save();
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using ComfortDev.DAL.Interfaces;
using ComfortDev.Common.Entities;
using ComfortDev.DAL.Repositories;

namespace ComfortDev.DAL.UnitsOfWork
{
    public class ComfortDevUnitOfWork : IUnitOfWork
    {
        private readonly ComfortDevContext db;
        private bool disposed = false;

        public ComfortDevUnitOfWork()
        {
            db = new ComfortDevContext();
            CourseTasks = new CourseTaskRepository(db);
            Tasks = new TaskRepository(db);
            TestAnswers = new TestAnswerRepository(db);
            TestQuestions = new TestQuestionRepository(db);
            Topics = new TopicRepository(db);
            Users = new UserRepository(db);
            UserCourses = new UserCourseRepository(db);
        }
        public IRepository<CourseTask> CourseTasks { get; }
        public IRepository<Task> Tasks { get; }
        public IRepository<TestAnswer> TestAnswers { get; }
        public IRepository<TestQuestion> TestQuestions { get; }
        public IRepository<Topic> Topics { get; }
        public IRepository<User> Users { get; }
        public IRepository<UserCourse> UserCourses { get; }

        public void Save()
        {
            db.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

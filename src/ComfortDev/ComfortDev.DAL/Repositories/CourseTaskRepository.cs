using System;
using System.Collections.Generic;
using System.Text;
using ComfortDev.DAL.Interfaces;
using ComfortDev.Common.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ComfortDev.DAL.Repositories
{
    class CourseTaskRepository: IRepository<CourseTask>
    {
        private readonly ComfortDevContext db;

        public CourseTaskRepository(ComfortDevContext context)
        {
            db = context;
        }

        public void Create(CourseTask item)
        {
            db.CourseTasks.Add(item);
        }

        public void Delete(int id)
        {
            var courseTask = db.CourseTasks.Find(id);
            if (courseTask != null)
            {
                db.CourseTasks.Remove(courseTask);
            }
        }

        public IEnumerable<CourseTask> Find(Func<CourseTask, bool> predicate)
        {
            return db.CourseTasks.Where(predicate).ToList();
        }

        public CourseTask Get(int id)
        {
            return db.CourseTasks.Find(id);
        }

        public IEnumerable<CourseTask> GetAll()
        {
            return db.CourseTasks;
        }

        public void Update(CourseTask item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}

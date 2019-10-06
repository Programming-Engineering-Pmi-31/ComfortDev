using System;
using System.Collections.Generic;
using System.Text;
using ComfortDev.DAL.Interfaces;
using ComfortDev.Common.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ComfortDev.DAL.Repositories
{
    class UserCourseRepository: IRepository<UserCourse>
    {
        private readonly ComfortDevContext db;

        public UserCourseRepository(ComfortDevContext context)
        {
            db = context;
        }

        public void Create(UserCourse item)
        {
            db.UserCourses.Add(item);
        }

        public void Delete(int id)
        {
            var userCourse = db.UserCourses.Find(id);
            if (userCourse != null)
            {
                db.UserCourses.Remove(userCourse);
            }
        }

        public IEnumerable<UserCourse> Find(Func<UserCourse, bool> predicate)
        {
            return db.UserCourses.Where(predicate).ToList();
        }

        public UserCourse Get(int id)
        {
            return db.UserCourses.Find(id);
        }

        public IEnumerable<UserCourse> GetAll()
        {
            return db.UserCourses;
        }

        public void Update(UserCourse item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}

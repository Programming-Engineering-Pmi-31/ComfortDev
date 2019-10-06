using System;
using System.Collections.Generic;
using System.Text;
using ComfortDev.DAL.Interfaces;
using ComfortDev.Common.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ComfortDev.DAL.Repositories
{
    class UserRepository: IRepository<User>
    {
        private readonly ComfortDevContext db;

        public UserRepository(ComfortDevContext context)
        {
            db = context;
        }

        public void Create(User item)
        {
            db.Users.Add(item);
        }

        public void Delete(int id)
        {
            var user = db.Users.Find(id);
            if (user != null)
            {
                db.Users.Remove(user);
            }
        }

        public IEnumerable<User> Find(Func<User, bool> predicate)
        {
            return db.Users.Where(predicate).ToList();
        }

        public User Get(int id)
        {
            return db.Users.Find(id);
        }

        public IEnumerable<User> GetAll()
        {
            return db.Users;
        }

        public void Update(User item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using ComfortDev.DAL.Interfaces;
using ComfortDev.DAL.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ComfortDev.DAL.Repositories
{
    class TopicRepository: IRepository<Topic>
    {
        private readonly ComfortDevContext db;

        public TopicRepository(ComfortDevContext context)
        {
            db = context;
        }

        public void Create(Topic item)
        {
            db.Topics.Add(item);
        }

        public void Delete(int id)
        {
            var topic = db.Topics.Find(id);
            if (topic != null)
            {
                db.Topics.Remove(topic);
            }
        }

        public IEnumerable<Topic> Find(Func<Topic, bool> predicate)
        {
            return db.Topics.Where(predicate).ToList();
        }

        public Topic Get(int id)
        {
            return db.Topics.Find(id);
        }

        public IEnumerable<Topic> GetAll()
        {
            return db.Topics;
        }

        public void Update(Topic item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}

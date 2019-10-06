using System;
using System.Collections.Generic;
using System.Text;
using ComfortDev.DAL.Interfaces;
using ComfortDev.Common.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ComfortDev.DAL.Repositories
{
    class TestAnswerRepository: IRepository<TestAnswer>
    {
        private readonly ComfortDevContext db;

        public TestAnswerRepository(ComfortDevContext context)
        {
            db = context;
        }

        public void Create(TestAnswer item)
        {
            db.TestAnswers.Add(item);
        }

        public void Delete(int id)
        {
            var testAnswer = db.TestAnswers.Find(id);
            if (testAnswer != null)
            {
                db.TestAnswers.Remove(testAnswer);
            }
        }

        public IEnumerable<TestAnswer> Find(Func<TestAnswer, bool> predicate)
        {
            return db.TestAnswers.Where(predicate).ToList();
        }

        public TestAnswer Get(int id)
        {
            return db.TestAnswers.Find(id);
        }

        public IEnumerable<TestAnswer> GetAll()
        {
            return db.TestAnswers;
        }

        public void Update(TestAnswer item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}

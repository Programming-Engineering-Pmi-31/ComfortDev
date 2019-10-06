using System;
using System.Collections.Generic;
using System.Text;
using ComfortDev.DAL.Interfaces;
using ComfortDev.Common.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ComfortDev.DAL.Repositories
{
    class TestQuestionRepository: IRepository<TestQuestion>
    {
        private readonly ComfortDevContext db;

        public TestQuestionRepository(ComfortDevContext context)
        {
            db = context;
        }

        public void Create(TestQuestion item)
        {
            db.TestQuestions.Add(item);
        }

        public void Delete(int id)
        {
            var testQuestion = db.TestQuestions.Find(id);
            if (testQuestion != null)
            {
                db.TestQuestions.Remove(testQuestion);
            }
        }

        public IEnumerable<TestQuestion> Find(Func<TestQuestion, bool> predicate)
        {
            return db.TestQuestions.Where(predicate).ToList();
        }

        public TestQuestion Get(int id)
        {
            return db.TestQuestions.Find(id);
        }

        public IEnumerable<TestQuestion> GetAll()
        {
            return db.TestQuestions;
        }

        public void Update(TestQuestion item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}

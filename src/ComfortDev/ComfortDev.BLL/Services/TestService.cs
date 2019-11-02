using ComfortDev.BLL.Interfaces;
using ComfortDev.Common.Entities;
using ComfortDev.DAL.UnitsOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComfortDev.BLL.Services
{
    public class TestService : ITestService
    {
        private readonly ComfortDevUnitOfWork database;
        public TestService(ComfortDevUnitOfWork uow)
        {
            database = uow;
        }
        public TestService() : this(new ComfortDevUnitOfWork()) { }

        public IEnumerable<TestQuestion> GetTest()
        {
            return database.TestQuestions.GetAll();
        }
    }
}

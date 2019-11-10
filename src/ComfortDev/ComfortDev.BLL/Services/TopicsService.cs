using ComfortDev.BLL.Interfaces;
using ComfortDev.Common.Entities;
using ComfortDev.DAL.UnitsOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComfortDev.BLL.Services
{
    public class TopicsService: ITopicsService
    {
        private readonly ComfortDevUnitOfWork database;
        public TopicsService(ComfortDevUnitOfWork uow)
        {
            database = uow;
        }
        public TopicsService() : this(new ComfortDevUnitOfWork()) { }

        public IEnumerable<Topic> GetTopics()
        {
            return database.Topics.GetAll();
        }
    }
}

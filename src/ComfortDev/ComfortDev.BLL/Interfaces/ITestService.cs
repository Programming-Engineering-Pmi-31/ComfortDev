using ComfortDev.Common.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComfortDev.BLL.Interfaces
{
    public interface ITestService
    {
        IEnumerable<TestQuestion> GetTest();
    }
}

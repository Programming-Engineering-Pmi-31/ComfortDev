using ComfortDev.Common.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComfortDev.BLL.Interfaces
{
    public interface IUserCoursesService
    {
        UserCourse CreateCourse(int userId, int topicId, int nTasks, int durationDays);
    }
}

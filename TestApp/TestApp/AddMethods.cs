using System;
using System.Collections.Generic;
using System.Text;

namespace TestApp
{
    partial class Program
    {
        static void AddCouseTask(ComfortDevTestContext db, List<string> args)
        {
            db.Add(new CourseTask { CourseId = int.Parse(args[0]), TaskId = int.Parse(args[1]), CompPer = int.Parse(args[2]) });
            db.SaveChanges();
        }
        static void AddTopic(ComfortDevTestContext db, List<string> args)
        {
            db.Add(new Topic { Title = args[0], ImageSource = args[1] });
            db.SaveChanges();
        }
        static void AddTask(ComfortDevTestContext db, List<string> args)
        {
            db.Add(new Task { Title = args[0], TopicId = int.Parse(args[1]), EvalCrit = args[2], TaskText = args[3] });
            db.SaveChanges();
        }
        static void AddTestAnswer(ComfortDevTestContext db, List<string> args)
        {
            db.Add(new TestAnswer { QuestionId = int.Parse(args[0]), Answer = args[1], TopicId = int.Parse(args[2]) });
            db.SaveChanges();
        }
        static void AddTestQuestion(ComfortDevTestContext db, List<string> args)
        {
            db.Add(new TestQuestion { Question = args[0] });
            db.SaveChanges();
        }
        static void AddUser(ComfortDevTestContext db, List<string> args)
        {
            db.Add(new User { Name = args[0], Hash = args[1] });
            db.SaveChanges();
        }
        static void AddUserCourse(ComfortDevTestContext db, List<string> args)
        {
            db.Add(new UserCourse { UserName = args[0], EndDate = DateTime.Parse(args[1]) });
            db.SaveChanges();
        }
    }
}

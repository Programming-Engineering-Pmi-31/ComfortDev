using System;
using System.Collections.Generic;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using var db = new ComfortDevTestContext();
            ShowTopics(db);
        }

        static void AddTopic(ComfortDevTestContext db, string title, string imageSource)
        {
            db.Add(new Topic { Title = title, ImageSource = imageSource });
            db.SaveChanges();
        }
        static void AddTask(ComfortDevTestContext db, string topicTitle, string taskTitle, string evalCrit)
        {
            int topicId = new List<Topic>(db.Topic).Find(elem => elem.Title == topicTitle).Id;
            db.Add(new Task { Title = taskTitle, TopicId = topicId, EvalCrit = evalCrit});
            db.SaveChanges();
        }

        // show tables
        static void ShowTopics(ComfortDevTestContext db)
        {
            Console.WriteLine("Topics:");
            foreach (Topic topic in db.Topic)
            {
                Console.WriteLine($"Id: {topic.Id}\t| Title: {topic.Title}\t| ImageSource: {topic.ImageSource}");
            }
            Console.WriteLine();
        }

        static void ShowUsers(ComfortDevTestContext db)
        {
            Console.WriteLine("Users:");
            foreach (User user in db.User)
            {
                Console.WriteLine($"Name: {user.Name}\t| Hash: {user.Hash}");
            }
            Console.WriteLine();
        }

        static void ShowTasks(ComfortDevTestContext db)
        {
            Console.WriteLine("Tasks:");
            foreach (Task task in db.Task)
            {
                Console.WriteLine($"Id: {task.Id}\t| Title: {task.Title}\t| TopicID: {task.TopicId}\t| EvalCrit: {task.EvalCrit}");
            }
            Console.WriteLine();
        }

        static void ShowCourseTasks(ComfortDevTestContext db)
        {
            Console.WriteLine("Course tasks:");
            foreach (CourseTask courseTask in db.CourseTask)
            {
                Console.WriteLine($"Id: {courseTask.Id}\t| CourseId: {courseTask.CourseId}\t| TaskID: {courseTask.TaskId}\t| CompPer: {courseTask.CompPer}");
            }
            Console.WriteLine();
        }

        static void ShowTestAnswers(ComfortDevTestContext db)
        {
            Console.WriteLine("Test Answers:");
            foreach (TestAnswer testAnswer in db.TestAnswer)
            {
                Console.WriteLine($"Id: {testAnswer.Id}\t| QuestionId: {testAnswer.QuestionId}\t| Answer: {testAnswer.Answer}\t| TopicId: {testAnswer.TopicId}");
            }
            Console.WriteLine();
        }

        static void ShowTestQuestions(ComfortDevTestContext db)
        {
            Console.WriteLine("Test Question:");
            foreach (TestQuestion testQuestion in db.TestQuestion)
            {
                Console.WriteLine($"Id: {testQuestion.Id}\t| Question: {testQuestion.Question}");
            }
            Console.WriteLine();
        }

        static void ShowUserCourses(ComfortDevTestContext db)
        {
            Console.WriteLine("User Courses:");
            foreach (UserCourse userCourse in db.UserCourse)
            {
                Console.WriteLine($"Id: {userCourse.Id}\t| UserName: {userCourse.UserName}\t| EndDate: {userCourse.EndDate}");
            }
            Console.WriteLine();
        }
    }
}

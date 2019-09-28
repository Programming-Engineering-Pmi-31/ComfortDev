using System;
using System.Collections.Generic;
using System.Text;

namespace TestApp
{
    partial class Program
    {
        static void ShowTopics(ComfortDevTestContext db)
        {
            Console.WriteLine("Topics:");
            foreach (Topic topic in db.Topics)
            {
                Console.WriteLine($"Id: {topic.Id}\t| Title: {topic.Title}\t| ImageSource: {topic.ImageSource}");
            }
        }

        static void ShowUsers(ComfortDevTestContext db)
        {
            Console.WriteLine("Users:");
            foreach (User user in db.Users)
            {
                Console.WriteLine($"Name: {user.Name}\t| Hash: {user.Hash}");
            }
        }

        static void ShowTasks(ComfortDevTestContext db)
        {
            Console.WriteLine("Tasks:");
            foreach (Task task in db.Tasks)
            {
                Console.WriteLine($"Id: {task.Id}\t| Title: {task.Title}\t| TopicID: {task.TopicId}\t | TaskText: {task.TaskText}\t| EvalCrit: {task.EvalCrit}");
            }
        }

        static void ShowCourseTasks(ComfortDevTestContext db)
        {
            Console.WriteLine("Course tasks:");
            foreach (CourseTask courseTask in db.CourseTasks)
            {
                Console.WriteLine($"Id: {courseTask.Id}\t| CourseId: {courseTask.CourseId}\t| TaskID: {courseTask.TaskId}\t| CompPer: {courseTask.CompPer}");
            }
        }

        static void ShowTestAnswers(ComfortDevTestContext db)
        {
            Console.WriteLine("Test Answers:");
            foreach (TestAnswer testAnswer in db.TestAnswers)
            {
                Console.WriteLine($"Id: {testAnswer.Id}\t| QuestionId: {testAnswer.QuestionId}\t| Answer: {testAnswer.Answer}\t| TopicId: {testAnswer.TopicId}");
            }
        }

        static void ShowTestQuestions(ComfortDevTestContext db)
        {
            Console.WriteLine("Test Question:");
            foreach (TestQuestion testQuestion in db.TestQuestions)
            {
                Console.WriteLine($"Id: {testQuestion.Id}\t| Question: {testQuestion.Question}");
            }
        }

        static void ShowUserCourses(ComfortDevTestContext db)
        {
            Console.WriteLine("User Courses:");
            foreach (UserCourse userCourse in db.UserCourses)
            {
                Console.WriteLine($"Id: {userCourse.Id}\t| UserName: {userCourse.UserName}\t| EndDate: {userCourse.EndDate}");
            }
        }
    }
}

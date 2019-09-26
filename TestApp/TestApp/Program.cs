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
    }
}

using System;

namespace TestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ComfortDevTestContext db = new ComfortDevTestContext())
            {
                var topics = db.Topics;
                foreach (var topic in topics)
                {
                    Console.WriteLine(topic.Title);
                }
                Console.Read();
            }
        }
    }
}

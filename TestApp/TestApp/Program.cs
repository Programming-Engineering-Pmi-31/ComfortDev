using System;
using System.Collections.Generic;

namespace TestApp
{
    partial class Program
    {
        static Dictionary<string, Action> actions = new Dictionary<string, Action>
        {
            { "coursetask", new Action(ShowCourseTasks, AddCouseTask) },
            { "task", new Action(ShowTasks, AddTask) },
            { "testanswer", new Action(ShowTestAnswers, AddTestAnswer) },
            { "testquestion", new Action(ShowTestQuestions, AddTestQuestion) },
            { "topic", new Action(ShowTopics, AddTopic) },
            { "user", new Action(ShowUsers, AddUser) },
            { "usercourse", new Action(ShowUserCourses, AddUserCourse)}
        };
        const int windowWidth = 120;
        static void Main(string[] args)
        {
            Console.WindowWidth = windowWidth;
            using var db = new ComfortDevTestContext();
            WriteHead();
            while (true)
            {
                WriteSep();
                var input = Console.ReadLine().Split();
                try
                {
                    var command = input[0].ToLower();
                    if (command == "show")
                    {
                        actions[input[1].ToLower().TrimEnd('s')].Show(db);
                    }
                    else if (command == "add")
                    {
                        var inputArgs = new List<string>(input).GetRange(2, input.Length - 2);
                        actions[input[1].ToLower()].Add(db, inputArgs);
                    }
                    else if (command == "clear")
                    {
                        Console.Clear();
                        WriteHead();
                    }
                    else if (command == "exit")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Unknown command.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
        static void WriteSep()
        {
            for (int i = 0; i < windowWidth; ++i)
            {
                Console.Write('-');
            }
            Console.Write('\n');
        }
        static void WriteHead()
        {
            Console.Write(
                "DataBase test editor\n" +
                "type \"add <element_name> <arg1, arg2, ...>\" to add element\n" +
                "type \"show <table_name>\" to show table\n"
            );
        }
    }
}

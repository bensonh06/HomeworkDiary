using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkDiary
{
    internal class Program
    {

        struct homeworkTask
        {
            public string title;
            public string description;
            public string dueDate;
        }

        static void Main(string[] args)
        {
            List<homeworkTask> homeworkTasks = new List<homeworkTask>();

            StreamReader homeworkReader = new StreamReader("homework.txt");

            while (true)
            {
                if (!homeworkReader.EndOfStream)
                {
                    homeworkTask task = new homeworkTask();

                    task.title = homeworkReader.ReadLine();
                    task.description = homeworkReader.ReadLine();
                    task.dueDate = homeworkReader.ReadLine();

                    homeworkReader.ReadLine();

                    homeworkTasks.Add(task);
                } else {
                    break; 
                }
            }

            homeworkReader.Close();

            while (true)
            {
                Console.Clear();

                Console.WriteLine("HOMEWORK DIARY\n");

                Console.WriteLine("Please choose an option: ");
                Console.WriteLine("1. View Homework");
                Console.WriteLine("2. Add Homework");
                Console.WriteLine("3. Complete Homework");
                Console.WriteLine("4. Exit Homework Diary");

                switch (Console.ReadLine())
                {
                    case "1":
                        viewHomework(homeworkTasks);
                        break;

                    case "2":
                        homeworkTask newTask = addHomework();

                        homeworkTasks.Add(newTask);

                        StreamWriter homeworkWriter = new StreamWriter("homework.txt");

                        for (int i = 0; i < homeworkTasks.Count; i++)
                        {
                            homeworkWriter.WriteLine(homeworkTasks[i].title);
                            homeworkWriter.WriteLine(homeworkTasks[i].description);
                            homeworkWriter.WriteLine(homeworkTasks[i].dueDate + "\n");


                        }

                        homeworkWriter.Close();

                        break;

                    case "3":
                        completeHomework(homeworkTasks);
                        break;

                    case "4":
                        exitDiary(homeworkTasks);
                        break;

                    default:

                        
                        break;

                }
            }
        }

        static void viewHomework(List<homeworkTask> list)
        {
            Console.Clear();

            for (int i = 0; i < list.Count; i++)
            {
                if ((DateTime.Parse(list[i].dueDate) - DateTime.Today).Days > 3)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                } else
                {
                    Console.ForegroundColor= ConsoleColor.Red;
                }

                Console.WriteLine("\nTitle: " + list[i].title);
                Console.WriteLine("Description: " + list[i].description);
                Console.WriteLine("Due Date: " + list[i].dueDate);
            }

            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("\nPress enter to return to the main menu...");
            Console.ReadLine();
        }

        static void completeHomework(List<homeworkTask> list)
        {
            while (true)
            {
                Console.Clear();

                for (int i = 0; i < list.Count; i++)
                {
                    Console.WriteLine("\nTitle: " + list[i].title);
                    Console.WriteLine("Description: " + list[i].description);
                    Console.WriteLine("Due Date: " + list[i].dueDate);
                }

                Console.WriteLine("\nPlease enter the title of the homework you would like to mark as completed, or enter c to cancel: ");

                string chosen = Console.ReadLine();

                if (chosen.ToLower() == "c")
                {
                    return;
                }

                int foundTask = -1;

                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].title == chosen)
                    {
                        foundTask = i;
                        break;
                    }
                }

                if (foundTask == -1)
                {
                    Console.WriteLine("\nUnable to find a task of that name.");
                }
                else
                {
                    Console.WriteLine("\nAre you sure you would like to delete this task? (y/n)");
                    if (Console.ReadLine().ToLower() == "y")
                    {
                        list.Remove(list[foundTask]);
                        Console.WriteLine("\nTask removed successfully.");
                    }
                }

                StreamWriter homeworkWriter = new StreamWriter("homework.txt");

                for (int i = 0; i < list.Count; i++)
                {
                    homeworkWriter.WriteLine(list[i].title);
                    homeworkWriter.WriteLine(list[i].description);
                    homeworkWriter.WriteLine(list[i].dueDate + "\n");


                }

                homeworkWriter.Close();

                Console.WriteLine("\nWould you like to remove another task? (y/n)");

                if (Console.ReadLine().ToLower() == "n")
                {
                    break;
                }
            }
        }

        static homeworkTask addHomework()
        {

            homeworkTask newTask = new homeworkTask();

            while (true)
            {
                

                Console.Clear();

                Console.WriteLine("Please enter the homework's title: ");

                newTask.title = Console.ReadLine();

                Console.WriteLine("\nPlease enter the homework's description: ");

                newTask.description = Console.ReadLine();

                while (true)
                {

                    Console.WriteLine("\nPlease enter the homework's due date: ");

                    try
                    {
                        newTask.dueDate = DateTime.Parse(Console.ReadLine()).ToShortDateString();
                    }
                    catch
                    {
                        continue;
                    }

                    if (DateTime.Parse(newTask.dueDate).CompareTo(DateTime.Today) < 0)
                    {
                        continue;
                    }

                    break;
                }

                Console.Clear();

                Console.WriteLine("Homework Preview: ");

                Console.WriteLine("\nTitle: " + newTask.title);
                Console.WriteLine("\nDescription: " + newTask.description);
                Console.WriteLine("\nDue Date: " + newTask.dueDate);

                Console.WriteLine("\nIs this correct? (y/n)");
                
                if (Console.ReadLine().ToLower() == "y")
                {



                    break;
                }
            }

            return newTask;
        }

        static void exitDiary(List<homeworkTask> list)
        {
            StreamWriter homeworkWriter = new StreamWriter("homework.txt");

            for (int i = 0; i < list.Count; i++)
            {
                homeworkWriter.WriteLine(list[i].title);
                homeworkWriter.WriteLine(list[i].description);
                homeworkWriter.WriteLine(list[i].dueDate + "\n");

                
            }

            homeworkWriter.Close();

            Environment.Exit(0);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Diagnostics.Metrics;
using System.Globalization;

namespace TaskManager
{
    class TaskManager
    {
        public static List<TaskItem> taskslist = new List<TaskItem>();
        public static int nbtasks = 0;
        public static void launcher()
        {
            bool loop = true;
            while (loop)
            {
                Console.WriteLine("=================================");
                Console.WriteLine("     Welcome to Task Manager     ");
                Console.WriteLine("=================================");
                Console.WriteLine("1. Create Task");
                Console.WriteLine("2. View Tasks");
                Console.WriteLine("3. Delete Task");
                Console.WriteLine("4. Update Task");
                Console.WriteLine("5. Save tasks to JSON");
                Console.WriteLine("6. Load tasks from JSON");
                Console.WriteLine("7. Exit");
                Console.WriteLine("Enter your choice: ");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        createTask();
                        break;
                    case 2:
                        viewTasks();
                        break;
                    case 3:
                        deleteTask();
                        break;
                    case 4:
                        updateTask();
                        break;
                    case 5:
                        savetoJson();
                        break;
                    case 6:
                        loadFromJson();
                        break;
                    case 7:
                        loop = false;
                        break;
                }
            }
        }
        static void createTask()
        {
            Console.WriteLine("Name of the task ?");
            String name = Console.ReadLine();
            Console.WriteLine("Description of task");    
            String desc = Console.ReadLine();
            Console.WriteLine("What Priority ? (Number)");
            int prio = Convert.ToInt32(Console.ReadLine());

            TaskItem newTask = new TaskItem
            {
                Id = nbtasks + 1,
                Title = name,
                Description = desc,
                DueDate = DateTime.Now,
                Priority = prio,
                IsCompleted = false
            };

            nbtasks += 1;
            taskslist.Add(newTask);
            Console.Clear();
            Console.WriteLine("Task added successfully, Id to show the task it " + nbtasks);
        }

        static void viewTasks()
        {
            int id = 0;
            Console.WriteLine("What tasks would you like to see ?");
            try
            {
                id = Convert.ToInt32(Console.ReadLine());
            } catch (FormatException) 
            { 
                Console.WriteLine("Please enter a valid number");
                return;
            }

            Console.Clear();

            TaskItem foundtask = taskslist.Find(x => x.Id == id);
            if (foundtask != null)
            {
                Console.WriteLine(foundtask.Id);
                Console.WriteLine(foundtask.Title);
                Console.WriteLine(foundtask.Description);
                Console.WriteLine(foundtask.DueDate);
                Console.WriteLine(foundtask.Priority);
                Console.WriteLine(foundtask.IsCompleted);
            }

        }
        static void deleteTask()
        {
            int deleteid = 0;
            Console.WriteLine("What tasks would you like to delete ? (Put the task Id)");
            try
            {
                deleteid = Convert.ToInt32(Console.ReadLine());
            } catch (FormatException)
            {
                Console.WriteLine("Please enter a valid Id"); 
                return;
            }

            TaskItem find = taskslist.Find(task => task.Id == deleteid);

            if (find != null)
            {
                Console.WriteLine("Here is the task you want to delete");
                Console.WriteLine(find.Id);
                Console.WriteLine(find.Title);
                Console.WriteLine(find.Description);
                Console.WriteLine(find.DueDate);
                Console.WriteLine(find.Priority);
                Console.WriteLine(find.IsCompleted);
            } else
            {
                Console.WriteLine("No tasks for the Id mentionned were found");
                return;
            }

                Console.WriteLine("Are you sure you want to delete the task ?");
            String action = Console.ReadLine().ToLower().Trim();

            if (action.Equals("yes"))
            {
                taskslist.RemoveAt(deleteid - 1);
                Console.WriteLine("Task Deleted succefully");
                Console.Clear();
                return;
            } 
            else if (action.Equals("no"))
            {
                Console.WriteLine("Action aborted");
                Console.Clear();
                return;
            } 
            else
            {
                Console.WriteLine("Invalid Action");
                return;
            }
        }
        static void updateTask()
        {
            bool loop = true;
            Console.WriteLine("What task would you like to update ?");
            int id = Convert.ToInt32(Console.ReadLine());
            TaskItem task = taskslist.Find(x => x.Id == id);
            
            if (task == null)
            {
                Console.WriteLine("No task found for the Id mentionned");
                return;
            }
            
            while (loop)
            {
                Console.WriteLine("What part of the task would you like to update ?");
                Console.WriteLine("1. Title");
                Console.WriteLine("2. Description");
                Console.WriteLine("3. Due Date");
                Console.WriteLine("4. Priority");
                Console.WriteLine("5. Is Completed");
                Console.WriteLine("6. Exit");
                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Enter the new title");
                        task.Title = Console.ReadLine();
                        break;
                    case 2:
                        Console.WriteLine("Enter the new description");
                        task.Description = Console.ReadLine();
                        break;
                    case 3:
                        Console.WriteLine("Enter the new due date");
                        task.DueDate = Convert.ToDateTime(Console.ReadLine());
                        break;
                    case 4:
                        Console.WriteLine("Enter the new priority");
                        task.Priority = Convert.ToInt32(Console.ReadLine());
                        break;
                    case 5:
                        Console.WriteLine("Enter the new status");
                        task.IsCompleted = Convert.ToBoolean(Console.ReadLine());
                        break;
                    case 6:
                        loop = false;
                        Console.Clear();
                        break;
                }
            }
        }

        static void savetoJson()
        {
            Console.WriteLine("Saving tasks to JSON...");
            Thread.Sleep(2000);
            string jsonString = JsonSerializer.Serialize(taskslist);
            File.WriteAllText(@"C:\Users\steam deck\source\repos\TaskManager\", jsonString);
            return;
        }

        static void loadFromJson()
        {

        }
    }
}

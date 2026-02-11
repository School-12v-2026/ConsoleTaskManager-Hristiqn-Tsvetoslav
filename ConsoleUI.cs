using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleTaskManager
{

    public class ConsoleUI
    {
        private readonly TaskService _service;

        public ConsoleUI(TaskService service)
        {
            _service = service;
        }

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                ShowHeader();
                ShowMenu();

                Console.Write("Избор: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": AddTask(); break;
                    case "2": ShowTasks(); break;
                    case "3": CompleteTask(); break;
                    case "4": DeleteTask(); break;
                    case "5": SortMenu(); break;
                    case "0": return;
                    default:
                        ShowMessage("Невалиден избор!", ConsoleColor.Red);
                        Pause();
                        break;
                }
            }
        }

        private void ShowHeader()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== Console Task Manager ===");
            Console.ResetColor();
            Console.WriteLine();
        }

        private void ShowMenu()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("1) Добави задача");
            Console.WriteLine("2) Покажи всички задачи");
            Console.WriteLine("3) Маркирай като завършена");
            Console.WriteLine("4) Изтрий задача");
            Console.WriteLine("5) Сортиране");
            Console.WriteLine("0) Изход");
            Console.ResetColor();
            Console.WriteLine();
        }

        private void AddTask()
        {
            Console.Write("Име на задача: ");
            string name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                ShowMessage("Името не може да е празно!", ConsoleColor.Red);
                Pause();
                return;
            }

            _service.Add(name);
            ShowMessage("Задачата е добавена!", ConsoleColor.Green);
            Pause();
        }

        private void ShowTasks()
        {
            Console.Clear();
            ShowHeader();

            var tasks = _service.GetAll();

            if (tasks.Count == 0)
            {
                ShowMessage("Няма задачи.", ConsoleColor.Red);
                Pause();
                return;
            }

            Console.WriteLine("ID | Име | Статус");
            Console.WriteLine("-----------------------");

            foreach (var task in tasks)
            {
                Console.ForegroundColor = task.IsCompleted
                    ? ConsoleColor.Green
                    : ConsoleColor.Red;

                Console.WriteLine($"{task.Id} | {task.Name} | {(task.IsCompleted ? "Завършена" : "Незавършена")}");
                Console.ResetColor();
            }

            Pause();
        }

        private void CompleteTask()
        {
            ShowTasks();
            int id = ReadInt("ID за завършване: ");

            if (_service.MarkCompleted(id))
                ShowMessage("Маркирана като завършена!", ConsoleColor.Green);
            else
                ShowMessage("Няма такава задача!", ConsoleColor.Red);

            Pause();
        }

        private void DeleteTask()
        {
            ShowTasks();
            int id = ReadInt("ID за изтриване: ");

            if (_service.Delete(id))
                ShowMessage("Изтрита успешно!", ConsoleColor.Green);
            else
                ShowMessage("Няма такава задача!", ConsoleColor.Red);

            Pause();
        }

        private void SortMenu()
        {
            Console.Clear();
            ShowHeader();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("1) Сортиране по име (A-Z)");
            Console.WriteLine("2) Незавършени първо");
            Console.WriteLine("0) Назад");
            Console.ResetColor();

            Console.Write("Избор: ");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                _service.SortByName();
                ShowMessage("Сортирано по име!", ConsoleColor.Green);
            }
            else if (choice == "2")
            {
                _service.SortByStatus();
                ShowMessage("Сортирано по статус!", ConsoleColor.Green);
            }

            Pause();
        }

        private int ReadInt(string message)
        {
            while (true)
            {
                Console.Write(message);
                if (int.TryParse(Console.ReadLine(), out int number))
                    return number;

                ShowMessage("Въведи валидно число!", ConsoleColor.Red);
            }
        }

        private void ShowMessage(string msg, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(msg);
            Console.ResetColor();
        }
        //dsadadad
        private void Pause()
        {
            Console.WriteLine("\nНатисни ENTER...");
            Console.ReadLine();
        }
    }
}
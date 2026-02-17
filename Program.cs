using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTaskManager
{
    internal class Program
    {
        static void Main(string[] args)
        {
        
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var repository = new TaskRepository("tasks.txt");
            var service = new TaskService(repository);
            var ui = new ConsoleUI(service);

            ui.Run();
            //dsadad
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTaskManager
{
    public class TodoTask
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public bool IsCompleted { get; set; }

        public TodoTask(int id, string name, bool completed = false)
        {
            Id = id;
            Name = name;
            IsCompleted = completed;
        }
    }
}

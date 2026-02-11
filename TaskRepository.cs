using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace ConsoleTaskManager
{
    public class TaskRepository
    {
        private readonly string _filePath;

        public TaskRepository(string filePath)
        {
            _filePath = filePath;
        }

        public List<TodoTask> Load()
        {
            var tasks = new List<TodoTask>();

            if (!File.Exists(_filePath))
                return tasks;

            var lines = File.ReadAllLines(_filePath);

            foreach (var line in lines)
            {
                var parts = line.Split('|');
                int id = int.Parse(parts[0]);
                bool completed = bool.Parse(parts[1]);
                string name = parts[2];

                tasks.Add(new TodoTask(id, name, completed));
            }

            return tasks;
        }

        public void Save(List<TodoTask> tasks)
        {
            var lines = tasks.Select(t =>
                $"{t.Id}|{t.IsCompleted}|{t.Name}");

            File.WriteAllLines(_filePath, lines);
        }
    }
}

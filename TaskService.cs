using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTaskManager
{
    public class TaskService
    {
        private readonly TaskRepository _repo;
        private List<TodoTask> _tasks;
        private int _nextId;

        public TaskService(TaskRepository repo)
        {
            _repo = repo;
            _tasks = _repo.Load();
            _nextId = _tasks.Count == 0 ? 1 : _tasks.Max(t => t.Id) + 1;
        }

        public List<TodoTask> GetAll() => _tasks;

        public void Add(string name)
        {
            _tasks.Add(new TodoTask(_nextId++, name));
            _repo.Save(_tasks);
        }

        public bool Delete(int id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                _tasks.Remove(task);
                _repo.Save(_tasks);
                return true;
            }

            return false;
        }

        public bool MarkCompleted(int id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                task.IsCompleted = true;
                _repo.Save(_tasks);
                return true;
            }

            return false;
        }

        public void SortByName()
        {
            _tasks = _tasks.OrderBy(t => t.Name).ToList();
            _repo.Save(_tasks);
        }

        public void SortByStatus()
        {
            _tasks = _tasks.OrderBy(t => t.IsCompleted).ToList();
            _repo.Save(_tasks);
        }
    }
}

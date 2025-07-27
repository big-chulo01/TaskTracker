using TaskTracker.Domain;
using TaskTracker.Domain.Interfaces;

namespace TaskTracker.Infrastructure.Repositories
{
    public class InMemoryTaskRepository : ITaskRepository
    {
        private readonly List<TaskItem> _tasks = new();
        private int _nextId = 1;

        public TaskItem Add(TaskItem task)
        {
            task = new TaskItem(_nextId++, task.Name, task.CreatedOn);
            _tasks.Add(task);
            return task;
        }

        public IEnumerable<TaskItem> GetAll() => _tasks.AsReadOnly();
        public TaskItem GetById(int id) => _tasks.FirstOrDefault(t => t.Id == id);
        
        public void Update(TaskItem task)
        {
            var existing = GetById(task.Id);
            if (existing != null)
            {
                _tasks.Remove(existing);
                _tasks.Add(task);
            }
        }
    }
}
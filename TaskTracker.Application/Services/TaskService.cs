// Services/TaskService.cs
using TaskTracker.Domain;
using TaskTracker.Domain.Interfaces;
using TaskTracker.Application.Interfaces;


namespace TaskTracker.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public TaskItem AddTask(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Task name cannot be empty");

            var task = new TaskItem(
                id: _taskRepository.GetAll().Count() + 1,
                name: name,
                createdOn: DateTime.Now
            );

            return _taskRepository.Add(task);
        }

        public IEnumerable<TaskItem> ListTasks()
        {
            return _taskRepository.GetAll();
        }

        public void CompleteTask(int id)
        {
            var task = _taskRepository.GetById(id) ?? 
                throw new ArgumentException("Task not found");

            task.MarkComplete();
            _taskRepository.Update(task);
        }
    }
}
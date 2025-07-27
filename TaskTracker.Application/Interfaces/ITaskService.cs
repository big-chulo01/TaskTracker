// Interfaces/ITaskService.cs
using TaskTracker.Domain;

namespace TaskTracker.Application.Interfaces
{
    public interface ITaskService
    {
        TaskItem AddTask(string name);
        IEnumerable<TaskItem> ListTasks();
        void CompleteTask(int id);
    }
}


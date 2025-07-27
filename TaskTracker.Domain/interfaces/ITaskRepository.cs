namespace TaskTracker.Domain.Interfaces
{
    public interface ITaskRepository
    {
        TaskItem Add(TaskItem task);
        IEnumerable<TaskItem> GetAll();
        TaskItem GetById(int id);
        void Update(TaskItem task);
    }
}
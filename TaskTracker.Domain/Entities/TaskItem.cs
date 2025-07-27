namespace TaskTracker.Domain
{
    public class TaskItem
    {
        public int Id { get; }
        public string Name { get; }
        public bool IsCompleted { get; private set; }
        public DateTime CreatedOn { get; }

        public TaskItem(int id, string name, DateTime createdOn)
        {
            Id = id;
            Name = name;
            CreatedOn = createdOn;
        }

        public void MarkComplete() => IsCompleted = true;
    }
}
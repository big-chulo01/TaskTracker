// Program.cs
using Microsoft.Extensions.DependencyInjection;
using TaskTracker.Application.Interfaces;
using TaskTracker.Domain;
using TaskTracker.Infrastructure;

var serviceProvider = new ServiceCollection()
    .AddInfrastructure()
    .BuildServiceProvider();

var taskService = serviceProvider.GetRequiredService<ITaskService>();

while (true)
{
    Console.Clear();
    Console.WriteLine("Task Tracker Application");
    Console.WriteLine("1. View Tasks");
    Console.WriteLine("2. Add New Task");
    Console.WriteLine("3. Mark Task Complete");
    Console.WriteLine("4. Exit");
    Console.Write("Select an option: ");

    var choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            ViewTasks(taskService);
            break;
        case "2":
            AddTask(taskService);
            break;
        case "3":
            CompleteTask(taskService);
            break;
        case "4":
            return;
        default:
            Console.WriteLine("Invalid option. Press any key to continue...");
            Console.ReadKey();
            break;
    }
}

static void ViewTasks(ITaskService taskService)
{
    var tasks = taskService.ListTasks();
    
    if (!tasks.Any())
    {
        Console.WriteLine("No tasks available.");
    }
    else
    {
        Console.WriteLine("ID\tName\t\tStatus\t\tCreated On");
        foreach (var task in tasks)
        {
            Console.WriteLine($"{task.Id}\t{task.Name}\t\t{(task.IsCompleted ? "Completed" : "Pending")}\t\t{task.CreatedOn:g}");
        }
    }
    
    Console.WriteLine("\nPress any key to continue...");
    Console.ReadKey();
}

static void AddTask(ITaskService taskService)
{
    Console.Write("Enter task name: ");
    var name = Console.ReadLine();
    
    try
    {
        var task = taskService.AddTask(name);
        Console.WriteLine($"Task added with ID: {task.Id}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
    
    Console.WriteLine("Press any key to continue...");
    Console.ReadKey();
}

static void CompleteTask(ITaskService taskService)
{
    Console.Write("Enter task ID to mark complete: ");
    if (int.TryParse(Console.ReadLine(), out int id))
    {
        try
        {
            taskService.CompleteTask(id);
            Console.WriteLine($"Task {id} marked as complete.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
    else
    {
        Console.WriteLine("Invalid ID format.");
    }
    
    Console.WriteLine("Press any key to continue...");
    Console.ReadKey();
}
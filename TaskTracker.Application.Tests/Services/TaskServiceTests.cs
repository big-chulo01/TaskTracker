// TaskServiceTests.cs
using Moq;
using TaskTracker.Application.Interfaces;
using TaskTracker.Application.Services;
using TaskTracker.Domain;
using TaskTracker.Domain.Interfaces;
using Xunit;

namespace TaskTracker.Application.Tests
{
    public class TaskServiceTests
    {
        private readonly Mock<ITaskRepository> _mockRepo;
        private readonly ITaskService _taskService;

        public TaskServiceTests()
        {
            _mockRepo = new Mock<ITaskRepository>();
            _taskService = new TaskService(_mockRepo.Object);
        }

        [Fact]
        public void AddTask_WithValidName_ShouldReturnTask()
        {
            // Arrange
            var taskName = "Valid Task";
            var expectedTask = new TaskItem(1, taskName, DateTime.Now);
            
            _mockRepo.Setup(r => r.GetAll()).Returns(new List<TaskItem>());
            _mockRepo.Setup(r => r.Add(It.IsAny<TaskItem>())).Returns(expectedTask);

            // Act
            var result = _taskService.AddTask(taskName);

            // Assert
            Assert.Equal(expectedTask, result);
            _mockRepo.Verify(r => r.Add(It.IsAny<TaskItem>()), Times.Once);
        }

        [Fact]
        public void AddTask_WithEmptyName_ShouldThrowException()
        {
            // Arrange
            var emptyName = "";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _taskService.AddTask(emptyName));
            _mockRepo.Verify(r => r.Add(It.IsAny<TaskItem>()), Times.Never);
        }

        [Fact]
        public void ListTasks_ShouldReturnAllTasks()
        {
            // Arrange
            var expectedTasks = new List<TaskItem>
            {
                new TaskItem(1, "Task 1", DateTime.Now),
                new TaskItem(2, "Task 2", DateTime.Now)
            };
            
            _mockRepo.Setup(r => r.GetAll()).Returns(expectedTasks);

            // Act
            var result = _taskService.ListTasks();

            // Assert
            Assert.Equal(expectedTasks, result);
            _mockRepo.Verify(r => r.GetAll(), Times.Once);
        }

        [Fact]
        public void CompleteTask_WithValidId_ShouldMarkTaskComplete()
        {
            // Arrange
            var task = new TaskItem(1, "Test Task", DateTime.Now);
            _mockRepo.Setup(r => r.GetById(1)).Returns(task);

            // Act
            _taskService.CompleteTask(1);

            // Assert
            Assert.True(task.IsCompleted);
            _mockRepo.Verify(r => r.Update(task), Times.Once);
        }

        [Fact]
        public void CompleteTask_WithInvalidId_ShouldThrowException()
        {
            // Arrange
            _mockRepo.Setup(r => r.GetById(It.IsAny<int>())).Returns((TaskItem)null);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _taskService.CompleteTask(999));
            _mockRepo.Verify(r => r.Update(It.IsAny<TaskItem>()), Times.Never);
        }
    }
}
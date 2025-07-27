using TaskTracker.Domain;
using TaskTracker.Application.Interfaces;
using TaskTracker.Application.Services;
using TaskTracker.Domain.Interfaces;
using Moq;
using Xunit;

namespace TaskTracker.Tests
{
    public class TaskServiceTests
    {
        // Domain Layer Tests
        public class TaskItemTests
        {
            [Fact]
            public void TaskItem_Constructor_SetsPropertiesCorrectly()
            {
                // Arrange
                var id = 1;
                var name = "Test Task";
                var createdOn = DateTime.Now;

                // Act
                var task = new TaskItem(id, name, createdOn);

                // Assert
                Assert.Equal(id, task.Id);
                Assert.Equal(name, task.Name);
                Assert.Equal(createdOn, task.CreatedOn);
                Assert.False(task.IsCompleted);
            }

            [Fact]
            public void MarkComplete_SetsIsCompletedToTrue()
            {
                // Arrange
                var task = new TaskItem(1, "Test Task", DateTime.Now);

                // Act
                task.MarkComplete();

                // Assert
                Assert.True(task.IsCompleted);
            }
        }

        // Application Layer Tests
        public class TaskServiceFunctionalityTests
        {
            private readonly Mock<ITaskRepository> _mockRepo;
            private readonly ITaskService _taskService;

            public TaskServiceFunctionalityTests()
            {
                _mockRepo = new Mock<ITaskRepository>();
                _taskService = new TaskService(_mockRepo.Object);
            }

            [Fact]
            public void AddTask_ValidName_ReturnsTask()
            {
                // Arrange
                var expectedTask = new TaskItem(1, "Valid Task", DateTime.Now);
                _mockRepo.Setup(r => r.GetAll()).Returns(new List<TaskItem>());
                _mockRepo.Setup(r => r.Add(It.IsAny<TaskItem>())).Returns(expectedTask);

                // Act
                var result = _taskService.AddTask("Valid Task");

                // Assert
                Assert.Equal(expectedTask, result);
            }

            [Fact]
            public void CompleteTask_ValidId_MarksTaskComplete()
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
        }

        // Edge Case Tests
        public class TaskServiceEdgeCases
        {
            private readonly Mock<ITaskRepository> _mockRepo;
            private readonly ITaskService _taskService;

            public TaskServiceEdgeCases()
            {
                _mockRepo = new Mock<ITaskRepository>();
                _taskService = new TaskService(_mockRepo.Object);
            }

            [Fact]
            public void AddTask_EmptyName_ThrowsException()
            {
                Assert.Throws<ArgumentException>(() => _taskService.AddTask(""));
            }

            [Fact]
            public void CompleteTask_InvalidId_ThrowsException()
            {
                _mockRepo.Setup(r => r.GetById(It.IsAny<int>())).Returns((TaskItem)null);
                Assert.Throws<ArgumentException>(() => _taskService.CompleteTask(999));
            }
        }
    }
}
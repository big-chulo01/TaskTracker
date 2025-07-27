// TaskItemTests.cs
using TaskTracker.Domain;
using Xunit;

namespace TaskTracker.Domain.Tests
{
    public class TaskItemTests
    {
        [Fact]
        public void Constructor_ShouldInitializeProperties()
        {
            // Arrange & Act
            var task = new TaskItem(1, "Test Task", DateTime.Now);
            
            // Assert
            Assert.Equal(1, task.Id);
            Assert.Equal("Test Task", task.Name);
            Assert.False(task.IsCompleted);
        }

        [Fact]
        public void MarkComplete_ShouldSetIsCompletedToTrue()
        {
            // Arrange
            var task = new TaskItem(1, "Test Task", DateTime.Now);
            
            // Act
            task.MarkComplete();
            
            // Assert
            Assert.True(task.IsCompleted);
        }
    }
}
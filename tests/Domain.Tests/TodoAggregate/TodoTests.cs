using Domain.TodoAggregate;
using Domain.TodoAggregate.Commands;

namespace Domain.Tests.TodoAggregate;

public class TodoTests
{
    [Fact]
    public void Create_WithValidCommand_SetsPropertiesCorrectly()
    {
        // Arrange
        var description = "Valid Todo Description";
        var dueDate = DateTime.Now.AddDays(5);
        var command = new CreateTodoCommand { Description = description, DueDate = dueDate };

        // Act
        var todo = new Todo(command);

        // Assert
        Assert.Equal(description, todo.Description);
        Assert.Equal(dueDate, todo.DueDate);
        Assert.Null(todo.CompletedAt);
        Assert.False(todo.Removed);
    }

    [Fact]
    public void Create_WithoutDueDate_SetsDueDateToNull()
    {
        // Arrange
        var description = "Todo without due date";
        var command = new CreateTodoCommand { Description = description };

        // Act
        var todo = new Todo(command);

        // Assert
        Assert.Null(todo.DueDate);
    }

    [Fact]
    public void Update_WithDescriptionOnly_UpdatesDescription()
    {
        // Arrange
        var originalDescription = "Original Description";
        var createCommand = new CreateTodoCommand { Description = originalDescription };
        var todo = new Todo(createCommand);
        var newDescription = "Updated Description";
        var updateCommand = new UpdateTodoCommand { Id = todo.Id, Description = newDescription };

        // Act
        var result = todo.Update(updateCommand);

        // Assert
        Assert.Same(todo, result);
        Assert.Equal(newDescription, todo.Description);
        Assert.Equal(createCommand.DueDate, todo.DueDate);
    }

    [Fact]
    public void Update_WithDueDateOnly_UpdatesDueDate()
    {
        // Arrange
        var originalDueDate = DateTime.Now.AddDays(5);
        var createCommand = new CreateTodoCommand { Description = "Test Todo", DueDate = originalDueDate };
        var todo = new Todo(createCommand);
        var newDueDate = DateTime.Now.AddDays(10);
        var updateCommand = new UpdateTodoCommand { Id = todo.Id, DueDate = newDueDate };

        // Act
        var result = todo.Update(updateCommand);

        // Assert
        Assert.Same(todo, result);
        Assert.Equal(newDueDate, todo.DueDate);
        Assert.Equal("Test Todo", todo.Description);
    }

    [Fact]
    public void Update_WithBothDescriptionAndDueDate_UpdatesBoth()
    {
        // Arrange
        var createCommand = new CreateTodoCommand { Description = "Original", DueDate = DateTime.Now.AddDays(5) };
        var todo = new Todo(createCommand);
        var newDescription = "Updated Description";
        var newDueDate = DateTime.Now.AddDays(15);
        var updateCommand = new UpdateTodoCommand { Id = todo.Id, Description = newDescription, DueDate = newDueDate };

        // Act
        var result = todo.Update(updateCommand);

        // Assert
        Assert.Same(todo, result);
        Assert.Equal(newDescription, todo.Description);
        Assert.Equal(newDueDate, todo.DueDate);
    }

    [Fact]
    public void Update_WithNullDescription_KeepsOriginalDescription()
    {
        // Arrange
        var originalDescription = "Keep This";
        var createCommand = new CreateTodoCommand { Description = originalDescription, DueDate = DateTime.Now.AddDays(5) };
        var todo = new Todo(createCommand);
        var updateCommand = new UpdateTodoCommand { Id = todo.Id, DueDate = DateTime.Now.AddDays(10) };

        // Act
        todo.Update(updateCommand);

        // Assert
        Assert.Equal(originalDescription, todo.Description);
    }

    [Fact]
    public void Update_WithNullDueDate_KeepsOriginalDueDate()
    {
        // Arrange
        var originalDueDate = DateTime.Now.AddDays(5);
        var createCommand = new CreateTodoCommand { Description = "Test", DueDate = originalDueDate };
        var todo = new Todo(createCommand);
        var updateCommand = new UpdateTodoCommand { Id = todo.Id, Description = "New Description" };

        // Act
        todo.Update(updateCommand);

        // Assert
        Assert.Equal(originalDueDate, todo.DueDate);
    }

    [Fact]
    public void Complete_SetsCompletedAt()
    {
        // Arrange
        var command = new CreateTodoCommand { Description = "Task to complete" };
        var todo = new Todo(command);
        var completedAt = DateTime.Now.AddMinutes(-5);
        var completeCommand = new CompleteTodoCommand { Id = todo.Id, CompletedAt = completedAt };

        // Act
        var result = todo.Complete(completeCommand);

        // Assert
        Assert.Same(todo, result);
        Assert.Equal(completedAt, todo.CompletedAt);
        Assert.False(todo.Removed);
    }

    [Fact]
    public void Complete_WithDefaultTime_SetsCompletedAt()
    {
        // Arrange
        var command = new CreateTodoCommand { Description = "Task to complete" };
        var todo = new Todo(command);

        // Act
        var completeCommand = new CompleteTodoCommand { Id = todo.Id };
        var result = todo.Complete(completeCommand);

        // Assert
        Assert.Same(todo, result);
        Assert.NotNull(todo.CompletedAt);
        // Verify it's approximately now (within 1 second)
        Assert.True(todo.CompletedAt <= DateTime.Now);
    }

    [Fact]
    public void Remove_SetsRemovedFlag()
    {
        // Arrange
        var command = new CreateTodoCommand { Description = "Task to remove" };
        var todo = new Todo(command);
        var removeCommand = new RemoveTodoCommand { Id = todo.Id };
        Assert.False(todo.Removed);

        // Act
        var result = todo.Remove(removeCommand);

        // Assert
        Assert.Same(todo, result);
        Assert.True(todo.Removed);
        Assert.Null(todo.CompletedAt);
    }

    [Fact]
    public void Remove_DoesNotAffectOtherProperties()
    {
        // Arrange
        var description = "Important Task";
        var dueDate = DateTime.Now.AddDays(7);
        var command = new CreateTodoCommand { Description = description, DueDate = dueDate };
        var todo = new Todo(command);
        var removeCommand = new RemoveTodoCommand { Id = todo.Id };

        // Act
        todo.Remove(removeCommand);

        // Assert
        Assert.Equal(description, todo.Description);
        Assert.Equal(dueDate, todo.DueDate);
        Assert.Null(todo.CompletedAt);
    }

    [Fact]
    public void MultipleOperations_ChainCorrectly()
    {
        // Arrange
        var createCommand = new CreateTodoCommand { Description = "Complex Task", DueDate = DateTime.Now.AddDays(10) };
        var todo = new Todo(createCommand);

        // Act
        var updateCommand = new UpdateTodoCommand { Id = todo.Id, Description = "Updated Task" };
        todo.Update(updateCommand);

        var completeCommand = new CompleteTodoCommand { Id = todo.Id };
        todo.Complete(completeCommand);

        var removeCommand = new RemoveTodoCommand { Id = todo.Id };
        todo.Remove(removeCommand);

        // Assert
        Assert.Equal("Updated Task", todo.Description);
        Assert.NotNull(todo.CompletedAt);
        Assert.True(todo.Removed);
    }
}

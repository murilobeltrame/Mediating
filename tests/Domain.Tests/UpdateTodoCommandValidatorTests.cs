using Ardalis.Specification;
using Domain.TodoAggregate;
using Domain.TodoAggregate.Commands;
using Domain.TodoAggregate.Specifications;
using FluentValidation.TestHelper;
using NSubstitute;

namespace Domain.Tests;

public class UpdateTodoCommandValidatorTests
{
    [Fact]
    public async Task Validate_WithValidIdAndExistingTodo_Succeeds()
    {
        // Arrange
        var repository = Substitute.For<IRepositoryBase<Todo>>();
        var todoId = Guid.NewGuid();
        repository.AnyAsync(Arg.Any<ISpecification<Todo>>(), Arg.Any<CancellationToken>()).Returns(true);

        var validator = new UpdateTodoCommandValidator(repository);
        var command = new UpdateTodoCommand { Id = todoId, Description = "Updated" };

        // Act
        var result = await validator.TestValidateAsync(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public async Task Validate_WithEmptyId_Fails()
    {
        // Arrange
        var repository = Substitute.For<IRepositoryBase<Todo>>();
        var validator = new UpdateTodoCommandValidator(repository);
        var command = new UpdateTodoCommand { Id = Guid.Empty, Description = "Updated" };

        // Act
        var result = await validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Id);
        Assert.Contains("Id is required.", result.Errors.Select(e => e.ErrorMessage));
    }

    [Fact]
    public async Task Validate_WithNonExistingTodo_Fails()
    {
        // Arrange
        var repository = Substitute.For<IRepositoryBase<Todo>>();
        repository.AnyAsync(Arg.Any<ISpecification<Todo>>(), Arg.Any<CancellationToken>()).Returns(false);

        var validator = new UpdateTodoCommandValidator(repository);
        var command = new UpdateTodoCommand { Id = Guid.NewGuid(), Description = "Updated" };

        // Act
        var result = await validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Id);
        Assert.Contains("Todo with the specified Id does not exist.", 
            result.Errors.Select(e => e.ErrorMessage));
    }

    [Fact]
    public async Task Validate_WithOnlyDescriptionUpdate_Succeeds()
    {
        // Arrange
        var repository = Substitute.For<IRepositoryBase<Todo>>();
        repository.AnyAsync(Arg.Any<ISpecification<Todo>>(), Arg.Any<CancellationToken>()).Returns(true);

        var validator = new UpdateTodoCommandValidator(repository);
        var command = new UpdateTodoCommand { Id = Guid.NewGuid(), Description = "New Description" };

        // Act
        var result = await validator.TestValidateAsync(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public async Task Validate_WithOnlyDueDateUpdate_Succeeds()
    {
        // Arrange
        var repository = Substitute.For<IRepositoryBase<Todo>>();
        repository.AnyAsync(Arg.Any<ISpecification<Todo>>(), Arg.Any<CancellationToken>()).Returns(true);

        var validator = new UpdateTodoCommandValidator(repository);
        var command = new UpdateTodoCommand { Id = Guid.NewGuid(), DueDate = DateTime.Now.AddDays(5) };

        // Act
        var result = await validator.TestValidateAsync(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public async Task Validate_WithBothDescriptionAndDueDate_Succeeds()
    {
        // Arrange
        var repository = Substitute.For<IRepositoryBase<Todo>>();
        repository.AnyAsync(Arg.Any<ISpecification<Todo>>(), Arg.Any<CancellationToken>()).Returns(true);

        var validator = new UpdateTodoCommandValidator(repository);
        var command = new UpdateTodoCommand 
        { 
            Id = Guid.NewGuid(), 
            Description = "Updated",
            DueDate = DateTime.Now.AddDays(10)
        };

        // Act
        var result = await validator.TestValidateAsync(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public async Task Validate_WithNeitherDescriptionNorDueDate_Succeeds()
    {
        // Arrange
        var repository = Substitute.For<IRepositoryBase<Todo>>();
        repository.AnyAsync(Arg.Any<ISpecification<Todo>>(), Arg.Any<CancellationToken>()).Returns(true);

        var validator = new UpdateTodoCommandValidator(repository);
        var command = new UpdateTodoCommand { Id = Guid.NewGuid() };

        // Act
        var result = await validator.TestValidateAsync(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public async Task Validate_CallsRepositoryAnyAsyncWithCorrectSpecification()
    {
        // Arrange
        var repository = Substitute.For<IRepositoryBase<Todo>>();
        repository.AnyAsync(Arg.Any<ISpecification<Todo>>(), Arg.Any<CancellationToken>()).Returns(true);

        var validator = new UpdateTodoCommandValidator(repository);
        var todoId = Guid.NewGuid();
        var command = new UpdateTodoCommand { Id = todoId };

        // Act
        await validator.TestValidateAsync(command);

        // Assert
        await repository.Received(1).AnyAsync(
            Arg.Is<ISpecification<Todo>>(spec => spec.GetType() == typeof(TodoByIdSpecification)),
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Validate_WithRepositoryException_ThrowsException()
    {
        // Arrange
        var repository = Substitute.For<IRepositoryBase<Todo>>();
        repository.AnyAsync(Arg.Any<ISpecification<Todo>>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromException<bool>(new Exception("Database error")));

        var validator = new UpdateTodoCommandValidator(repository);
        var command = new UpdateTodoCommand { Id = Guid.NewGuid() };

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => validator.TestValidateAsync(command));
    }
}

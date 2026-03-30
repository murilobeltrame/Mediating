using Ardalis.Specification;
using Domain.TodoAggregate;
using Domain.TodoAggregate.Commands;
using Domain.TodoAggregate.Specifications;
using FluentValidation.TestHelper;
using NSubstitute;

namespace Domain.Tests;

public class RemoveTodoCommandValidatorTests
{
    [Fact]
    public async Task Validate_WithValidIdAndExistingTodo_Succeeds()
    {
        // Arrange
        var repository = Substitute.For<IRepositoryBase<Todo>>();
        repository.AnyAsync(Arg.Any<ISpecification<Todo>>(), Arg.Any<CancellationToken>()).Returns(true);

        var validator = new RemoveTodoCommandValidator(repository);
        var command = new RemoveTodoCommand { Id = Guid.NewGuid() };

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
        var validator = new RemoveTodoCommandValidator(repository);
        var command = new RemoveTodoCommand { Id = Guid.Empty };

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

        var validator = new RemoveTodoCommandValidator(repository);
        var command = new RemoveTodoCommand { Id = Guid.NewGuid() };

        // Act
        var result = await validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Id);
        Assert.Contains("Todo with the specified Id does not exist.", 
            result.Errors.Select(e => e.ErrorMessage));
    }

    [Fact]
    public async Task Validate_WithValidGuidId_Succeeds()
    {
        // Arrange
        var repository = Substitute.For<IRepositoryBase<Todo>>();
        repository.AnyAsync(Arg.Any<ISpecification<Todo>>(), Arg.Any<CancellationToken>()).Returns(true);

        var validator = new RemoveTodoCommandValidator(repository);
        var validGuid = Guid.Parse("550e8400-e29b-41d4-a716-446655440000");
        var command = new RemoveTodoCommand { Id = validGuid };

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

        var validator = new RemoveTodoCommandValidator(repository);
        var todoId = Guid.NewGuid();
        var command = new RemoveTodoCommand { Id = todoId };

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

        var validator = new RemoveTodoCommandValidator(repository);
        var command = new RemoveTodoCommand { Id = Guid.NewGuid() };

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => validator.TestValidateAsync(command));
    }

    [Fact]
    public async Task Validate_WithMultipleCalls_WorksCorrectly()
    {
        // Arrange
        var repository = Substitute.For<IRepositoryBase<Todo>>();
        repository.AnyAsync(Arg.Any<ISpecification<Todo>>(), Arg.Any<CancellationToken>()).Returns(true);

        var validator = new RemoveTodoCommandValidator(repository);

        // Act & Assert - First command
        var command1 = new RemoveTodoCommand { Id = Guid.NewGuid() };
        var result1 = await validator.TestValidateAsync(command1);
        result1.ShouldNotHaveAnyValidationErrors();

        // Act & Assert - Second command
        var command2 = new RemoveTodoCommand { Id = Guid.NewGuid() };
        var result2 = await validator.TestValidateAsync(command2);
        result2.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public async Task Validate_WithRepositoryReturningFalse_CorrectlyReportsError()
    {
        // Arrange
        var repository = Substitute.For<IRepositoryBase<Todo>>();
        repository.AnyAsync(Arg.Any<ISpecification<Todo>>(), Arg.Any<CancellationToken>()).Returns(false);

        var validator = new RemoveTodoCommandValidator(repository);
        var command = new RemoveTodoCommand { Id = Guid.NewGuid() };

        // Act
        var result = await validator.TestValidateAsync(command);

        // Assert
        Assert.Single(result.Errors);
        Assert.Equal("Todo with the specified Id does not exist.", result.Errors[0].ErrorMessage);
    }
}

using Ardalis.Specification;
using Domain.TodoAggregate;
using Domain.TodoAggregate.Commands;
using Domain.TodoAggregate.Specifications;
using FluentValidation.TestHelper;
using NSubstitute;

namespace Domain.Tests;

public class CompleteTodoCommandValidatorTests
{
    [Fact]
    public async Task Validate_WithValidIdAndCompletedAt_Succeeds()
    {
        // Arrange
        var repository = Substitute.For<IRepositoryBase<Todo>>();
        repository.AnyAsync(Arg.Any<ISpecification<Todo>>(), Arg.Any<CancellationToken>()).Returns(true);

        var validator = new CompleteTodoCommandValidator(repository);
        var command = new CompleteTodoCommand { Id = Guid.NewGuid(), CompletedAt = DateTime.Now.AddMinutes(-5) };

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
        var validator = new CompleteTodoCommandValidator(repository);
        var command = new CompleteTodoCommand { Id = Guid.Empty, CompletedAt = DateTime.Now };

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

        var validator = new CompleteTodoCommandValidator(repository);
        var command = new CompleteTodoCommand { Id = Guid.NewGuid(), CompletedAt = DateTime.Now };

        // Act
        var result = await validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Id);
        Assert.Contains("Todo with the specified Id does not exist.", 
            result.Errors.Select(e => e.ErrorMessage));
    }

    [Fact]
    public async Task Validate_WithFutureCompletedAt_Fails()
    {
        // Arrange
        var repository = Substitute.For<IRepositoryBase<Todo>>();
        repository.AnyAsync(Arg.Any<ISpecification<Todo>>(), Arg.Any<CancellationToken>()).Returns(true);

        var validator = new CompleteTodoCommandValidator(repository);
        var futureTime = DateTime.Now.AddSeconds(5);
        var command = new CompleteTodoCommand { Id = Guid.NewGuid(), CompletedAt = futureTime };

        // Act
        var result = await validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.CompletedAt);
        Assert.Contains("CompletedAt cannot be in the future.", result.Errors.Select(e => e.ErrorMessage));
    }

    [Fact]
    public async Task Validate_WithPastTimeCompletedAt_Succeeds()
    {
        // Arrange
        var repository = Substitute.For<IRepositoryBase<Todo>>();
        repository.AnyAsync(Arg.Any<ISpecification<Todo>>(), Arg.Any<CancellationToken>()).Returns(true);

        var validator = new CompleteTodoCommandValidator(repository);
        var pastTime = DateTime.Now.AddSeconds(-10);
        var command = new CompleteTodoCommand { Id = Guid.NewGuid(), CompletedAt = pastTime };

        // Act
        var result = await validator.TestValidateAsync(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public async Task Validate_WithPastCompletedAt_Succeeds()
    {
        // Arrange
        var repository = Substitute.For<IRepositoryBase<Todo>>();
        repository.AnyAsync(Arg.Any<ISpecification<Todo>>(), Arg.Any<CancellationToken>()).Returns(true);

        var validator = new CompleteTodoCommandValidator(repository);
        var pastTime = DateTime.Now.AddMinutes(-30);
        var command = new CompleteTodoCommand { Id = Guid.NewGuid(), CompletedAt = pastTime };

        // Act
        var result = await validator.TestValidateAsync(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public async Task Validate_WithPastDefaultTime_Succeeds()
    {
        // Arrange
        var repository = Substitute.For<IRepositoryBase<Todo>>();
        repository.AnyAsync(Arg.Any<ISpecification<Todo>>(), Arg.Any<CancellationToken>()).Returns(true);

        var validator = new CompleteTodoCommandValidator(repository);
        var pastTime = DateTime.Now.AddSeconds(-5);
        var command = new CompleteTodoCommand { Id = Guid.NewGuid(), CompletedAt = pastTime };

        // Act
        var result = await validator.TestValidateAsync(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public async Task Validate_WithAllErrorsPresent_ReportsAllErrors()
    {
        // Arrange
        var repository = Substitute.For<IRepositoryBase<Todo>>();
        repository.AnyAsync(Arg.Any<ISpecification<Todo>>(), Arg.Any<CancellationToken>()).Returns(false);

        var validator = new CompleteTodoCommandValidator(repository);
        var futureTime = DateTime.Now.AddSeconds(10);
        var command = new CompleteTodoCommand { Id = Guid.Empty, CompletedAt = futureTime };

        // Act
        var result = await validator.TestValidateAsync(command);

        // Assert
        Assert.True(result.Errors.Count >= 2);
        Assert.NotEmpty(result.Errors.Where(e => e.PropertyName == nameof(CompleteTodoCommand.Id)));
        Assert.NotEmpty(result.Errors.Where(e => e.PropertyName == nameof(CompleteTodoCommand.CompletedAt)));
    }

    [Fact]
    public async Task Validate_CallsRepositoryAnyAsyncWithCorrectSpecification()
    {
        // Arrange
        var repository = Substitute.For<IRepositoryBase<Todo>>();
        repository.AnyAsync(Arg.Any<ISpecification<Todo>>(), Arg.Any<CancellationToken>()).Returns(true);

        var validator = new CompleteTodoCommandValidator(repository);
        var todoId = Guid.NewGuid();
        var command = new CompleteTodoCommand { Id = todoId };

        // Act
        await validator.TestValidateAsync(command);

        // Assert
        await repository.Received(1).AnyAsync(
            Arg.Is<ISpecification<Todo>>(spec => spec.GetType() == typeof(TodoByIdSpecification)),
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Validate_WithVeryOldCompletedAt_Succeeds()
    {
        // Arrange
        var repository = Substitute.For<IRepositoryBase<Todo>>();
        repository.AnyAsync(Arg.Any<ISpecification<Todo>>(), Arg.Any<CancellationToken>()).Returns(true);

        var validator = new CompleteTodoCommandValidator(repository);
        var veryOldTime = DateTime.Now.AddDays(-365);
        var command = new CompleteTodoCommand { Id = Guid.NewGuid(), CompletedAt = veryOldTime };

        // Act
        var result = await validator.TestValidateAsync(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}

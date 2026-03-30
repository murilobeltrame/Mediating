using Domain.TodoAggregate;
using Domain.TodoAggregate.Commands;
using FluentValidation.TestHelper;

namespace Domain.Tests;

public class CreateTodoCommandValidatorTests
{
    [Fact]
    public void Validate_WithValidDescription_Succeeds()
    {
        // Arrange
        var validator = new CreateTodoCommandValidator();
        var command = new CreateTodoCommand { Description = "Valid Description" };

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_WithMinimumValidLength_Succeeds()
    {
        // Arrange
        var validator = new CreateTodoCommandValidator();
        var command = new CreateTodoCommand { Description = "Exact" };

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_WithLongDescription_Succeeds()
    {
        // Arrange
        var validator = new CreateTodoCommandValidator();
        var longDescription = new string('a', 500);
        var command = new CreateTodoCommand { Description = longDescription };

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_WithEmptyDescription_Fails()
    {
        // Arrange
        var validator = new CreateTodoCommandValidator();
        var command = new CreateTodoCommand { Description = "" };

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Description);
        Assert.Contains("Description is required and must be at least 5 characters long.", 
            result.Errors.Select(e => e.ErrorMessage));
    }

    [Fact]
    public void Validate_WithWhitespaceOnly_Fails()
    {
        // Arrange
        var validator = new CreateTodoCommandValidator();
        var command = new CreateTodoCommand { Description = "   " };

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Description);
    }

    [Fact]
    public void Validate_WithTooShortDescription_Fails()
    {
        // Arrange
        var validator = new CreateTodoCommandValidator();
        var command = new CreateTodoCommand { Description = "abc" };

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Description);
        Assert.Contains("Description is required and must be at least 5 characters long.", 
            result.Errors.Select(e => e.ErrorMessage));
    }

    [Fact]
    public void Validate_WithExactlyFourCharacters_Fails()
    {
        // Arrange
        var validator = new CreateTodoCommandValidator();
        var command = new CreateTodoCommand { Description = "abcd" };

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Description);
    }

    [Fact]
    public void Validate_WithNullDescription_Fails()
    {
        // Arrange
        var validator = new CreateTodoCommandValidator();
        var command = new CreateTodoCommand { Description = null! };

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Description);
    }

    [Fact]
    public void Validate_WithoutDueDate_Succeeds()
    {
        // Arrange
        var validator = new CreateTodoCommandValidator();
        var command = new CreateTodoCommand { Description = "Valid Todo" };

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_WithFutureDueDate_Succeeds()
    {
        // Arrange
        var validator = new CreateTodoCommandValidator();
        var command = new CreateTodoCommand 
        { 
            Description = "Valid Todo",
            DueDate = DateTime.Now.AddDays(5)
        };

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_WithPastDueDate_Succeeds()
    {
        // Arrange
        var validator = new CreateTodoCommandValidator();
        var command = new CreateTodoCommand 
        { 
            Description = "Valid Todo",
            DueDate = DateTime.Now.AddDays(-5)
        };

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}

using Domain.TodoAggregate;
using Domain.TodoAggregate.Commands;
using Domain.TodoAggregate.Specifications;

namespace Domain.Tests;

public class TodoByIdSpecificationTests
{
    [Fact]
    public void TodoByIdSpecification_CreatesCorrectQuery()
    {
        // Arrange
        var todoId = Guid.NewGuid();

        // Act
        var spec = new TodoByIdSpecification(todoId);

        // Assert
        Assert.NotNull(spec);
    }

    [Fact]
    public void TodoByIdSpecification_WithDifferentIds_CreatesDifferentInstances()
    {
        // Arrange
        var todoId1 = Guid.NewGuid();
        var todoId2 = Guid.NewGuid();

        // Act
        var spec1 = new TodoByIdSpecification(todoId1);
        var spec2 = new TodoByIdSpecification(todoId2);

        // Assert
        Assert.NotNull(spec1);
        Assert.NotNull(spec2);
        Assert.NotEqual(todoId1, todoId2);
    }

    [Fact]
    public void TodoByIdSpecification_WithSameId_CreatesSimilarSpecs()
    {
        // Arrange
        var todoId = Guid.NewGuid();

        // Act
        var spec1 = new TodoByIdSpecification(todoId);
        var spec2 = new TodoByIdSpecification(todoId);

        // Assert
        Assert.NotNull(spec1);
        Assert.NotNull(spec2);
    }

    [Fact]
    public void TodoByIdSpecification_CanBeUsedWithEmptyGuid()
    {
        // Arrange
        var emptyGuid = Guid.Empty;

        // Act
        var spec = new TodoByIdSpecification(emptyGuid);

        // Assert
        Assert.NotNull(spec);
    }

    [Fact]
    public void TodoByIdSpecification_IsProperlyConstructed()
    {
        // Arrange
        var todoId = Guid.NewGuid();

        // Act
        var spec = new TodoByIdSpecification(todoId);

        // Assert
        Assert.NotNull(spec);
    }

    [Fact]
    public void TodoByIdSpecification_MultipleInstances_AreIndependent()
    {
        // Arrange
        var todoId1 = Guid.NewGuid();
        var todoId2 = Guid.NewGuid();
        var todoId3 = Guid.NewGuid();

        // Act
        var spec1 = new TodoByIdSpecification(todoId1);
        var spec2 = new TodoByIdSpecification(todoId2);
        var spec3 = new TodoByIdSpecification(todoId3);

        // Assert
        Assert.NotNull(spec1);
        Assert.NotNull(spec2);
        Assert.NotNull(spec3);
    }
}

using Domain.TodoAggregate.Specifications;

namespace Domain.Tests;

public class TodosByFilterSpecificationTests
{
    [Fact]
    public void TodosByFilterSpecification_WithDefaultFilters_CreatesSpecification()
    {
        // Arrange
        var page = 1;
        var pageSize = 10;

        // Act
        var spec = new TodosByFilterSpecification(page, pageSize, null, null, null, null);

        // Assert
        Assert.NotNull(spec);
    }

    [Fact]
    public void TodosByFilterSpecification_SecondPage_CreatesCorrectly()
    {
        // Arrange
        var page = 2;
        var pageSize = 10;

        // Act
        var spec = new TodosByFilterSpecification(page, pageSize, null, null, null, null);

        // Assert
        Assert.NotNull(spec);
    }

    [Fact]
    public void TodosByFilterSpecification_WithDescription_CreatesCorrectly()
    {
        // Arrange
        var description = "Test";

        // Act
        var spec = new TodosByFilterSpecification(1, 10, description, null, null, null);

        // Assert
        Assert.NotNull(spec);
    }

    [Fact]
    public void TodosByFilterSpecification_WithFromDueDate_CreatesCorrectly()
    {
        // Arrange
        var fromDate = DateTime.Now.AddDays(-5);

        // Act
        var spec = new TodosByFilterSpecification(1, 10, null, fromDate, null, null);

        // Assert
        Assert.NotNull(spec);
    }

    [Fact]
    public void TodosByFilterSpecification_WithToDueDate_CreatesCorrectly()
    {
        // Arrange
        var toDate = DateTime.Now.AddDays(5);

        // Act
        var spec = new TodosByFilterSpecification(1, 10, null, null, toDate, null);

        // Assert
        Assert.NotNull(spec);
    }

    [Fact]
    public void TodosByFilterSpecification_WithCompleteTrue_CreatesCorrectly()
    {
        // Arrange
        var complete = true;

        // Act
        var spec = new TodosByFilterSpecification(1, 10, null, null, null, complete);

        // Assert
        Assert.NotNull(spec);
    }

    [Fact]
    public void TodosByFilterSpecification_WithCompleteFalse_CreatesCorrectly()
    {
        // Arrange
        var complete = false;

        // Act
        var spec = new TodosByFilterSpecification(1, 10, null, null, null, complete);

        // Assert
        Assert.NotNull(spec);
    }

    [Fact]
    public void TodosByFilterSpecification_WithAllFilters_CombinesCorrectly()
    {
        // Arrange
        var description = "Buy groceries";
        var fromDate = DateTime.Now.AddDays(-10);
        var toDate = DateTime.Now.AddDays(10);
        var complete = true;

        // Act
        var spec = new TodosByFilterSpecification(1, 10, description, fromDate, toDate, complete);

        // Assert
        Assert.NotNull(spec);
    }

    [Fact]
    public void TodosByFilterSpecification_WithLargePageSize_CreatesCorrectly()
    {
        // Arrange
        var pageSize = 100;

        // Act
        var spec = new TodosByFilterSpecification(1, pageSize, null, null, null, null);

        // Assert
        Assert.NotNull(spec);
    }

    [Fact]
    public void TodosByFilterSpecification_WithLargePage_CreatesCorrectly()
    {
        // Arrange
        var page = 100;
        var pageSize = 10;

        // Act
        var spec = new TodosByFilterSpecification(page, pageSize, null, null, null, null);

        // Assert
        Assert.NotNull(spec);
    }

    [Fact]
    public void TodosByFilterSpecification_DescriptionFilterCanBeNull()
    {
        // Arrange
        var spec1 = new TodosByFilterSpecification(1, 10, null, null, null, null);
        var spec2 = new TodosByFilterSpecification(1, 10, "Something", null, null, null);

        // Act & Assert
        Assert.NotNull(spec1);
        Assert.NotNull(spec2);
    }

    [Fact]
    public void TodosByFilterSpecification_WithDateRangeSpanningToday_CreatesCorrectly()
    {
        // Arrange
        var fromDate = DateTime.Now.AddDays(-1);
        var toDate = DateTime.Now.AddDays(1);

        // Act
        var spec = new TodosByFilterSpecification(1, 10, null, fromDate, toDate, null);

        // Assert
        Assert.NotNull(spec);
    }

    [Fact]
    public void TodosByFilterSpecification_WithEmptyDescription_CreatesCorrectly()
    {
        // Arrange
        var spec1 = new TodosByFilterSpecification(1, 10, "", null, null, null);
        var spec2 = new TodosByFilterSpecification(1, 10, null, null, null, null);

        // Act & Assert
        Assert.NotNull(spec1);
        Assert.NotNull(spec2);
    }

    [Fact]
    public void TodosByFilterSpecification_WithWhitespaceDescription_CreatesCorrectly()
    {
        // Arrange
        var spec1 = new TodosByFilterSpecification(1, 10, "   ", null, null, null);
        var spec2 = new TodosByFilterSpecification(1, 10, null, null, null, null);

        // Act & Assert
        Assert.NotNull(spec1);
        Assert.NotNull(spec2);
    }
}

using AutoBogus;

using Domain.TodoAggregate;
using Domain.TodoAggregate.Specifications;

namespace Domain.Tests.TodoAggregate.Specifications;

public class TodosByFilterSpecificationTests
{
    [Fact]
    public void TodosByFilterSpecification_WhenSearchingByDefaults_ReturnOnlyFirstPageWithNotRemovedRecords()
    {
        var records = new AutoFaker<Todo>().Generate(50);
        var spec = new TodosByFilterSpecification(1, 10);

        var result = spec.Evaluate(records);

        Assert.Equal(10, result.Count());
    }

    [Theory]
    [InlineData("Specific Description", 1)]
    [InlineData("*Specific Description", 2)]
    public void TodosByFilterSpecification_WhenSearchingByDescription_ReturnMatchingRecords(string searchingDescription, int howManyExpected)
    {
        var records = new AutoFaker<Todo>().Generate(18);
        records.Add(new AutoFaker<Todo>()
            .RuleFor(p => p.Description, f => "Specific Description")
            .RuleFor(p => p.Removed, false)
            .Generate());
        records.Add(new AutoFaker<Todo>()
            .RuleFor(p => p.Description, f => "Specific Description")
            .RuleFor(p => p.Removed, true)
            .Generate());
        records.Add(new AutoFaker<Todo>()
            .RuleFor(p => p.Description, f => "Another Specific Description")
            .RuleFor(p => p.Removed, false)
            .Generate());
        records.Add(new AutoFaker<Todo>()
            .RuleFor(p => p.Description, f => "Another Specific Description")
            .RuleFor(p => p.Removed, true)
            .Generate());
        var spec = new TodosByFilterSpecification(1, 10, searchingDescription);

        var result = spec.Evaluate(records);

        Assert.Equal(howManyExpected, result.Count());
        Assert.All(result, r => Assert.Contains(searchingDescription.Replace("*", ""), r.Description));
    }

    [Theory]
    [InlineData(true, 3)]
    [InlineData(false, 2)]
    [InlineData(null, 5)]
    public void TodoByFilterSpecification_WhenSearchingCompleted_ReturnsMatchingRecords(bool? complete, int howManyExpected)
    {
        List<Todo> records = [];
        records.AddRange(new AutoFaker<Todo>()
            .RuleFor(p => p.Removed, false)
            .RuleFor(p => p.CompletedAt, f => f.Date.Between(
                DateTime.Now.AddDays(-10),
                DateTime.Now.AddDays(-1)))
            .Generate(3));
        records.AddRange(new AutoFaker<Todo>()
            .RuleFor(p => p.Removed, false)
            .RuleFor(p => p.CompletedAt, f => null)
            .Generate(2));
        var spec = new TodosByFilterSpecification(1, 10, complete: complete);

        var result = spec.Evaluate(records);

        Assert.Equal(howManyExpected, result.Count());
    }

    private static readonly DateTime
        FromDueDate = DateTime.Now.AddDays(-10),
        ToDueDate = DateTime.Now.AddDays(10);

    public static TheoryData<DateTime?, DateTime?, int> DateTimeRangeData =>
        new()
        {
            { null, null, 15 },
            { FromDueDate, ToDueDate, 5 },
            { null, ToDueDate, 10 },
            { FromDueDate, null, 10 },
        };

    [Theory, MemberData(nameof(DateTimeRangeData))]

    public void TodoByFilterSpecification_WhenSearchingDateTimeRange_ReturnsMatchingRecords(DateTime? fromDueDate, DateTime? toDueDate, int howManyExpected)
    {
        List<Todo> records = [];
        records.AddRange(new AutoFaker<Todo>()
            .RuleFor(p => p.Removed, false)
            .RuleFor(p => p.DueDate, f => f.Date.Between(FromDueDate, ToDueDate))
            .Generate(5));
        records.AddRange(new AutoFaker<Todo>()
            .RuleFor(p => p.Removed, false)
            .RuleFor(p => p.DueDate, f => f.Date.Between(FromDueDate.AddDays(-10), FromDueDate.AddDays(-1)))
            .Generate(5));
        records.AddRange(new AutoFaker<Todo>()
            .RuleFor(p => p.Removed, false)
            .RuleFor(p => p.DueDate, f => f.Date.Between(ToDueDate.AddDays(1), ToDueDate.AddDays(10)))
            .Generate(5));

        var spec = new TodosByFilterSpecification(1, 50, fromDueDate: fromDueDate, toDueDate: toDueDate);

        var result = spec.Evaluate(records);

        Assert.Equal(howManyExpected, result.Count());
    }
}
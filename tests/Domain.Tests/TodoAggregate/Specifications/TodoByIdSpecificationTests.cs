using AutoBogus;

using Domain.TodoAggregate;
using Domain.TodoAggregate.Specifications;

namespace Domain.Tests.TodoAggregate.Specifications;

public class TodoByIdSpecificationTests
{
    [Fact]
    public void TodoByIdSpecification_WhenSearchingNotRemovedId_ReturnsMatchingRecord()
    {
        var record = new AutoFaker<Todo>()
            .RuleFor(p => p.Id, Guid.NewGuid())
            .RuleFor(p => p.Removed, false)
            .Generate();
        var records = new AutoFaker<Todo>().Generate(19);
        records.Add(record);
        var spec = new TodoByIdSpecification(record.Id);

        var result = spec.Evaluate(records);

        Assert.Single(result);
        Assert.Equal(record.Id, result.First().Id);
        Assert.Equal(record.Description, result.First().Description);
    }

    [Fact]
    public void TodoByIdSpecification_WhenSearchingRemovedId_Fails()
    {
        var record = new AutoFaker<Todo>()
            .RuleFor(p => p.Id, Guid.NewGuid())
            .RuleFor(p => p.Removed, true)
            .Generate();
        var records = new AutoFaker<Todo>().Generate(19);
        records.Add(record);
        var spec = new TodoByIdSpecification(record.Id);

        var result = spec.Evaluate(records);

        Assert.Empty(result);
    }
}

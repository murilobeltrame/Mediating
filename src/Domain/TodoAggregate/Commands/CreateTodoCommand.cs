using Domain.TodoAggregate.ValueObjects;

namespace Domain.TodoAggregate.Commands;

public class CreateTodoCommand
{
    public required string Description { get; init; }
    public DateTime? DueDate { get; init; }
    public string? Location { get; init; }
    public Coordinate? Coordinate { get; init; }
}

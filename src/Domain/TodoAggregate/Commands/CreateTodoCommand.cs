namespace Domain.TodoAggregate.Commands;

public class CreateTodoCommand
{
    public required string Description { get; init; }
    public DateTime? DueDate { get; init; }
}

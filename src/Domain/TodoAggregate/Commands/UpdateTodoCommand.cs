namespace Domain.TodoAggregate.Commands;

public class UpdateTodoCommand
{
    public Guid Id { get; init; }
    public string? Description { get; init; }
    public DateTime? DueDate { get; init; }
};
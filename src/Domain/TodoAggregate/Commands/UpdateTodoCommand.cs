namespace Domain.TodoAggregate.Commands;

public class UpdateTodoCommand
{
    public Guid Id { get; init; }
    public string? Description { get; set; }
    public DateTime? DueDate { get; init; }
};
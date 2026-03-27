namespace Domain.TodoAggregate.Commands;

public class CompleteTodoCommand
{
    public Guid Id { get; init; }
    public DateTime CompletedAt { get; init; } = DateTime.Now;
};

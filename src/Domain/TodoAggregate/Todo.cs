using Domain.TodoAggregate.Commands;

namespace Domain.TodoAggregate;

public class Todo
{
    public Guid Id { get; private set; }
    public string Description { get; private set; }
    public DateTime? DueDate { get; private set; }
    public DateTime? CompletedAt { get; private set; }
    public bool Removed { get; private set; } = false;

#pragma warning disable CS8618 // Required by EF.
    private Todo() { }
#pragma warning restore CS8618 // Required by EF.

    public Todo(CreateTodoCommand command)
    {
        Description = command.Description;
        DueDate = command.DueDate;
    }

    public Todo Update(UpdateTodoCommand command)
    {
        Description = command.Description ?? Description;
        DueDate = command.DueDate ?? DueDate;
        return this;
    }

    public Todo Complete(CompleteTodoCommand command)
    {
        CompletedAt = command.CompletedAt;
        return this;
    }

    public Todo Remove(RemoveTodoCommand _)
    {
        Removed = true;
        return this;
    }
}

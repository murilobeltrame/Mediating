using Domain.TodoAggregate.Commands;
using Domain.TodoAggregate.ValueObjects;

namespace Domain.TodoAggregate;

public class Todo
{
#pragma warning disable S1144 // Required by EF.
    public Guid Id { get; private set; }
#pragma warning restore S1144 // Required by EF.
    public string Description { get; private set; }
    public DateTime? DueDate { get; private set; }
    public DateTime? CompletedAt { get; private set; }
    public bool Removed { get; private set; } = false;
    public string? Location { get; private set; }
    public bool? Remote => Location?.Contains("://");
    public Coordinates? Coordinates { get; private set; }

#pragma warning disable CS8618 // Required by EF.
    private Todo() { }
#pragma warning restore CS8618 // Required by EF.

    public Todo(CreateTodoCommand command)
    {
        Description = command.Description;
        DueDate = command.DueDate;
        Location = command.Location;
        Coordinates = command.Coordinates;
    }

    public Todo Update(UpdateTodoCommand command)
    {
        Description = command.Description ?? Description;
        DueDate = command.DueDate ?? DueDate;
        if (!string.IsNullOrWhiteSpace(command.Location))
        {
            Location = command.Location;
            Coordinates = command.Coordinate;
        }
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

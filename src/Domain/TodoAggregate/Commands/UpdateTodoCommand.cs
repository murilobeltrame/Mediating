using Domain.TodoAggregate.ValueObjects;

namespace Domain.TodoAggregate.Commands;

public class UpdateTodoCommand
{
    public Guid Id { get; init; }
    public string? Description { get; init; }
    public DateTime? DueDate { get; init; }
    public string? Location { get; init; }
    public Coordinates? Coordinates { get; private set; }

    public UpdateTodoCommand WithCoordinates(Coordinates coordinates)
    {
        Coordinates = coordinates;
        return this;
    }

    public UpdateTodoCommand WithCoordinates(decimal latitude, decimal longitude) =>
        WithCoordinates(new Coordinates(latitude, longitude));
};
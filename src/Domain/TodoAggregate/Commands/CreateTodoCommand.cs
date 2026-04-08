using System.Text.Json.Serialization;

using Domain.TodoAggregate.ValueObjects;

namespace Domain.TodoAggregate.Commands;

public class CreateTodoCommand
{
    public required string Description { get; init; }
    public DateTime? DueDate { get; init; }
    public string? Location { get; init; }
    [JsonIgnore]
    public Coordinates? Coordinates { get; private set; }

    public CreateTodoCommand WithCoordinates(Coordinates coordinates)
    {
        Coordinates = coordinates;
        return this;
    }

    public CreateTodoCommand WithCoordinates(decimal latitude, decimal longitude) =>
        WithCoordinates(new Coordinates(latitude, longitude));
}

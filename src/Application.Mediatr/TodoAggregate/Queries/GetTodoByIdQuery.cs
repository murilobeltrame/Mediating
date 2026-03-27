using Domain.TodoAggregate;

using MediatR;

namespace Application.Mediatr.TodoAggregate.Queries;

public class GetTodoByIdQuery : IRequest<Todo>
{
    public Guid Id { get; init; }
}

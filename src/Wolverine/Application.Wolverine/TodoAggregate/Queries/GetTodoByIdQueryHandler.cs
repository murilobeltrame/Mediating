using Ardalis.Specification;

using Domain.TodoAggregate;

namespace Application.Wolverine.TodoAggregate.Queries;

public static class GetTodoByIdQueryHandler
{
    public static async Task<Todo> Handle(
        GetTodoByIdQuery request,
        IRepositoryBase<Todo> repository,
        CancellationToken cancellationToken) =>
        await repository.GetByIdAsync(request.Id, cancellationToken) ??
            throw new Exception($"Todo with id {request.Id} not found.");
}

public class GetTodoByIdQuery
{
    public Guid Id { get; init; }
}

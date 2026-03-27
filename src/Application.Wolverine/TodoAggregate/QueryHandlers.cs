using Application.Wolverine.TodoAggregate.Queries;

using Ardalis.Specification;

using Domain.TodoAggregate;
using Domain.TodoAggregate.Specifications;

namespace Application.Wolverine.TodoAggregate;

public static class QueryHandlers
{
    public static async Task<Todo> Handle(
        GetTodoByIdQuery request,
        IRepositoryBase<Todo> repository,
        CancellationToken cancellationToken) =>
        await repository.GetByIdAsync(request.Id, cancellationToken) ??
        throw new Exception($"Todo with id {request.Id} not found.");

    public static async Task<IEnumerable<SimplerTodo>> Handle(
        FetchTodosByFilterQuery request,
        IRepositoryBase<Todo> repository,
        CancellationToken cancellationToken) =>
        await repository.ListAsync(request.ToSpecification(), cancellationToken);
}

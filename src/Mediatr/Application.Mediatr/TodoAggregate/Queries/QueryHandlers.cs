using Ardalis.Specification;

using Domain.TodoAggregate;
using Domain.TodoAggregate.Specifications;

using MediatR;

namespace Application.Mediatr.TodoAggregate.Queries;

internal class QueryHandlers(IRepositoryBase<Todo> repository) :
    IRequestHandler<GetTodoByIdQuery, Todo>,
    IRequestHandler<FetchTodosByFilterQuery, IEnumerable<SimplerTodo>>
{
    public async Task<Todo> Handle(GetTodoByIdQuery request, CancellationToken cancellationToken) =>
        await repository.GetByIdAsync(request.Id, cancellationToken) ??
        throw new Exception($"Todo with id {request.Id} not found.");

    public async Task<IEnumerable<SimplerTodo>> Handle(FetchTodosByFilterQuery request, CancellationToken cancellationToken) =>
        await repository.ListAsync(request.ToSpecification(), cancellationToken);
}

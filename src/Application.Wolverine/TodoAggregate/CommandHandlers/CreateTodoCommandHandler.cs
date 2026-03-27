using Ardalis.Specification;

using Domain.TodoAggregate;
using Domain.TodoAggregate.Commands;

namespace Application.Wolverine.TodoAggregate.CommandHandlers;

public static class CreateTodoCommandHandler
{
    public static async Task<Todo> Handle(
        CreateTodoCommand command,
        IRepositoryBase<Todo> repository,
        CancellationToken cancellationToken)
    {
        var todo = new Todo(command);
        todo = await repository.AddAsync(todo, cancellationToken);
        return todo;
    }
}

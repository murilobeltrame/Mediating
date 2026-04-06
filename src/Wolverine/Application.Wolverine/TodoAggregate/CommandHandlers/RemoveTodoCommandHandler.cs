using Ardalis.Specification;

using Domain.TodoAggregate;
using Domain.TodoAggregate.Commands;

namespace Application.Wolverine.TodoAggregate.CommandHandlers;

public static class RemoveTodoCommandHandler
{
    public static async Task Handle(
        RemoveTodoCommand command,
        IRepositoryBase<Todo> repository,
        CancellationToken cancellationToken)
    {
        var todo = await repository.GetByIdAsync(command.Id, cancellationToken) ??
            throw new Exception($"Todo with id {command.Id} not found.");
        todo.Remove(command);
        await repository.UpdateAsync(todo, cancellationToken);
    }
}

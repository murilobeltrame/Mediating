using Ardalis.Specification;

using Domain.TodoAggregate;
using Domain.TodoAggregate.Commands;

namespace Application.Wolverine.TodoAggregate;

public static class CommandHandlers
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
    public static async Task Handle(
        UpdateTodoCommand command,
        IRepositoryBase<Todo> repository,
        CancellationToken cancellationToken)
    {
        var todo = await GetTodoById(repository, command.Id, cancellationToken);
        todo.Update(command);
        await repository.UpdateAsync(todo, cancellationToken);
    }

    public static async Task Handle(
        CompleteTodoCommand command,
        IRepositoryBase<Todo> repository,
        CancellationToken cancellationToken)
    {
        var todo = await GetTodoById(repository, command.Id, cancellationToken);
        todo.Complete(command);
        await repository.UpdateAsync(todo, cancellationToken);
    }

    public static async Task Handle(
        RemoveTodoCommand command,
        IRepositoryBase<Todo> repository,
        CancellationToken cancellationToken)
    {
        var todo = await GetTodoById(repository, command.Id, cancellationToken);
        todo.Remove(command);
        await repository.UpdateAsync(todo, cancellationToken);
    }

    private static async Task<Todo> GetTodoById(
        IRepositoryBase<Todo> repository,
        Guid id,
        CancellationToken cancellationToken) =>
        await repository.GetByIdAsync(id, cancellationToken) ??
            throw new Exception($"Todo with id {id} not found.");
}

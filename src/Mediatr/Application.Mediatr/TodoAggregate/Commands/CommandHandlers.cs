using Ardalis.Specification;

using Domain.TodoAggregate;

using MediatR;

namespace Application.Mediatr.TodoAggregate.Commands;

internal class CommandHandlers(IRepositoryBase<Todo> repository) :
    IRequestHandler<CreateTodoCommand, Todo>,
    IRequestHandler<UpdateTodoCommand>,
    IRequestHandler<CompleteTodoCommand>,
    IRequestHandler<RemoveTodoCommand>
{
    public async Task<Todo> Handle(CreateTodoCommand command, CancellationToken cancellationToken)
    {
        var todo = new Todo(command);
        todo = await repository.AddAsync(todo, cancellationToken);
        return todo;
    }

    public async Task Handle(UpdateTodoCommand command, CancellationToken cancellationToken)
    {
        var todo = await GetTodoById(command.Id, cancellationToken);
        todo.Update(command);
        await repository.UpdateAsync(todo, cancellationToken);
    }

    public async Task Handle(CompleteTodoCommand command, CancellationToken cancellationToken)
    {
        var todo = await GetTodoById(command.Id, cancellationToken);
        todo.Complete(command);
        await repository.UpdateAsync(todo, cancellationToken);
    }

    public async Task Handle(RemoveTodoCommand command, CancellationToken cancellationToken)
    {
        var todo = await GetTodoById(command.Id, cancellationToken);
        todo.Remove(command);
        await repository.UpdateAsync(todo, cancellationToken);
    }

    private async Task<Todo> GetTodoById(Guid id, CancellationToken cancellationToken) =>
        await repository.GetByIdAsync(id, cancellationToken) ??
            throw new Exception($"Todo with id {id} not found.");
}

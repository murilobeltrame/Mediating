using Domain.TodoAggregate;

using MediatR;

#pragma warning disable CS8981 // To avoid top level declarations conflicts.
using domain = Domain.TodoAggregate.Commands;
#pragma warning restore CS8981 // To avoid top level declarations conflicts.

namespace Application.Mediatr.TodoAggregate.Commands;

public class CreateTodoCommand : domain.CreateTodoCommand, IRequest<Todo>;

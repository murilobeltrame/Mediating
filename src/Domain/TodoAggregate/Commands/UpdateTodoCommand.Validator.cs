using Ardalis.Specification;

using Domain.TodoAggregate.Specifications;

using FluentValidation;

namespace Domain.TodoAggregate.Commands;

internal class UpdateTodoCommandValidator : AbstractValidator<UpdateTodoCommand>
{
    public UpdateTodoCommandValidator(IRepositoryBase<Todo> repository)
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .WithMessage("Id is required.");

        RuleFor(c => c.Id)
            .MustAsync(async (id, token) => await repository.AnyAsync(new TodoByIdSpecification(id), token))
            .WithMessage("Todo with the specified Id does not exist.");
    }
}

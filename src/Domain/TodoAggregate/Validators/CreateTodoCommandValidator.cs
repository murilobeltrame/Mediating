using Domain.TodoAggregate.Commands;

using FluentValidation;

namespace Domain.TodoAggregate.Validators;

public class CreateTodoCommandValidator : AbstractValidator<CreateTodoCommand>
{
    public CreateTodoCommandValidator()
    {
        RuleFor(c => c.Description)
            .NotEmpty()
            .MinimumLength(5)
            .WithMessage("Description is required and must be at least 5 characters long.");
    }
}

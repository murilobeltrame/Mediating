using FluentValidation;

namespace Domain.TodoAggregate.Commands;

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

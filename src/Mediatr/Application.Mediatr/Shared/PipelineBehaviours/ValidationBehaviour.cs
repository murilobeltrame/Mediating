using FluentValidation;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Application.Mediatr.Shared.PipelineBehaviours;

public class ValidationBehaviour<TRequest, TResponse>(
    ILogger<ValidationBehaviour<TRequest,TResponse>> logger,
    IEnumerable<IValidator<TRequest>> validators) :
    IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        logger.LogDebug("Calling Validation Behaviour");
        
        var context = new ValidationContext<TRequest>(request);
        var failures = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context)));
        var errors = failures.SelectMany(f => f.Errors).Where(f => f != null).ToList();

        return errors.Count != 0 ? throw new ValidationException(errors) : await next(cancellationToken);
    }
}

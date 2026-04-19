using Application.Shared;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Application.Mediatr.Shared.PipelineBehaviours;

public class EnrichmentBehaviour<TRequest, TResponse>(
    ILogger<EnrichmentBehaviour<TRequest, TResponse>> logger,
    IEnumerable<IEnricher<TRequest>> enrichers) : 
    IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    public async Task<TResponse> Handle(
        TRequest request, 
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
        logger.LogDebug("Calling Enrichment Behaviour");

        await Task.WhenAll(enrichers.Select(e => e.EnrichAsync(request, cancellationToken)));
        // TODO: Handle errors in bunch
        
        return await next(cancellationToken);
    }
}

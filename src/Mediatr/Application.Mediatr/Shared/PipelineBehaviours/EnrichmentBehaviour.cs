
using MediatR;

using Microsoft.Extensions.Logging;

namespace Application.Mediatr.Shared.PipelineBehaviours;

public class EnrichmentBehaviour<TRequest, TResponse>(ILogger<EnrichmentBehaviour<TRequest, TResponse>> logger) : 
    IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        logger.LogDebug("Calling Enrichment Behaviour");
        return await next(cancellationToken);
    }
}

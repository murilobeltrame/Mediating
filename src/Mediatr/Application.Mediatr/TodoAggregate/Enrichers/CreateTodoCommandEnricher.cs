using Acl.GeolocationService;

using Application.Mediatr.TodoAggregate.Commands;
using Application.Shared;

namespace Application.Mediatr.TodoAggregate.Enrichers;

public class CreateTodoCommandEnricher(ILocationService locationService) : IEnricher<CreateTodoCommand>
{
    public async Task<CreateTodoCommand> EnrichAsync(CreateTodoCommand item, CancellationToken cancellationToken) =>
        await EnrichGeoLocation(item, cancellationToken);

    public async Task<CreateTodoCommand> EnrichGeoLocation(CreateTodoCommand item, CancellationToken cancellationToken)
    {
        var noLocationOrRemote = 
            string.IsNullOrWhiteSpace(item.Location) ||
            item.Location.Contains("://", StringComparison.OrdinalIgnoreCase);

        if (noLocationOrRemote) return item;

        var location = await locationService.GetLocationAsync(item.Location!, cancellationToken);
        item.WithCoordinates(location.Latitude, location.Longitude);
        return item;
    }
}

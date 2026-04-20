using Acl.GeolocationService;

using Application.Mediatr.TodoAggregate.Commands;
using Application.Shared;

namespace Application.Mediatr.TodoAggregate.Enrichers;

public class UpdateTodoCommandEnricher(ILocationService locationService) : IEnricher<UpdateTodoCommand>
{
    public async Task<UpdateTodoCommand> EnrichAsync(UpdateTodoCommand item, CancellationToken cancellationToken) =>
        await EnrichGeoLocation(item, cancellationToken);

    protected async Task<UpdateTodoCommand> EnrichGeoLocation(UpdateTodoCommand item, CancellationToken cancellationToken)
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
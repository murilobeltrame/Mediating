namespace Acl.GeolocationService;

public record Location(decimal Latitude, decimal Longitude);

public interface ILocationService
{
    Task<Location> GetLocationAsync(string address, CancellationToken cancellationToken = default);
}

public class LocationService : ILocationService
{
    public Task<Location> GetLocationAsync(string address, CancellationToken cancellationToken = default)
    {
        // In a real implementation, this method would call an external geolocation API
        // to get the latitude and longitude for the given address. For this example,
        // we'll just return a fixed location for demonstration purposes.
        var location = new Location(40.7128m, -74.0060m); // Example: New York City coordinates
        return Task.FromResult(location);
    }
}

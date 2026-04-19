namespace Application.Shared;

public interface IEnricher<T>
{
    Task<T> EnrichAsync(T item, CancellationToken cancellationToken);
}

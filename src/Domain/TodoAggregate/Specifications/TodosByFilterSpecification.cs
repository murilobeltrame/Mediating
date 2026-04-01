using Ardalis.Specification;

namespace Domain.TodoAggregate.Specifications;

public class TodosByFilterSpecification : Specification<Todo, SimplerTodo>
{
    public TodosByFilterSpecification(
        int page,
        int pageSize,
        string? description = null,
        DateTime? fromDueDate = null,
        DateTime? toDueDate = null,
        bool? complete = null)
    {
        if (!string.IsNullOrWhiteSpace(description))
        {
            if (description.Contains('*'))
            {
                var searchDescription = description.Replace("*", "%");
                Query.Search(todo => todo.Description, searchDescription);
            }
            else
            {
                Query.Where(todo => todo.Description == description);
            }
        }
        if (fromDueDate.HasValue)
        {
            Query.Where(todo => todo.DueDate >= fromDueDate);
        }
        if (toDueDate.HasValue)
        {
            Query.Where(todo => todo.DueDate <= toDueDate);
        }
        if (complete.HasValue)
        {
            Query.Where(todo => todo.CompletedAt.HasValue == complete);
        }
        Query
            .Where(todo => todo.Removed == false)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(todo => new SimplerTodo
            {
                Id = todo.Id,
                Description = todo.Description,
                DueDate = todo.DueDate,
                CompletedAt = todo.CompletedAt
            });
    }
}

public class SimplerTodo
{
    public Guid Id { get; set; }
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime? CompletedAt { get; set; }
}
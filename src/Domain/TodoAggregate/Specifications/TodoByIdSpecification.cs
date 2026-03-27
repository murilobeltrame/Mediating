using Ardalis.Specification;

namespace Domain.TodoAggregate.Specifications;

public class TodoByIdSpecification : Specification<Todo>
{
    public TodoByIdSpecification(Guid id) =>
        Query.Where(todo => todo.Id == id && todo.Removed == false);
}

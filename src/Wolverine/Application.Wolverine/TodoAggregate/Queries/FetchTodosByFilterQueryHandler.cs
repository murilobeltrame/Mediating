using Ardalis.Specification;

using Domain.TodoAggregate;
using Domain.TodoAggregate.Specifications;

namespace Application.Wolverine.TodoAggregate.Queries;

public static class FetchTodosByFilterQueryHandler
{
    public static async Task<IEnumerable<SimplerTodo>> Handle(
       FetchTodosByFilterQuery request,
       IRepositoryBase<Todo> repository,
       CancellationToken cancellationToken) =>
       await repository.ListAsync(request.ToSpecification(), cancellationToken);
}

public class FetchTodosByFilterQuery
{
    const int PageDefault = 1;
    const int PageSizeDefault = 10;

    public string? Description { get; set; }
    public DateTime? FromDueDate { get; set; }
    public DateTime? ToDueDate { get; set; }
    public bool? Complete { get; set; }
    public int? Page { get; set; }
    public int? PageSize { get; set; }

    public TodosByFilterSpecification ToSpecification() =>
        new(Page ?? PageDefault, PageSize ?? PageSizeDefault, Description, FromDueDate, ToDueDate, Complete);
}

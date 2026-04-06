using Domain.TodoAggregate.Specifications;

using MediatR;

namespace Application.Mediatr.TodoAggregate.Queries;

public class FetchTodosByFilterQuery : IRequest<IEnumerable<SimplerTodo>>
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

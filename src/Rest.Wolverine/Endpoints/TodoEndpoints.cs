using Application.Wolverine.TodoAggregate.Queries;

using Domain.TodoAggregate;
using Domain.TodoAggregate.Commands;
using Domain.TodoAggregate.Specifications;


using Microsoft.AspNetCore.Mvc;

using Wolverine;

namespace Rest.Wolverine.Endpoints;

public static class TodoEndpoints
{
    internal static async Task<IEnumerable<SimplerTodo>> Fetch(
        [AsParameters] FetchTodosByFilterQuery query,
        [FromServices] IMessageBus bus) =>
        await bus.InvokeAsync<IEnumerable<SimplerTodo>>(query);

    internal static async Task<Todo> Create(
        [FromBody] CreateTodoCommand command,
        [FromServices] IMessageBus bus) =>
        await bus.InvokeAsync<Todo>(command);

    internal static async Task<Todo> Get(
        [FromRoute] Guid id,
        [FromServices] IMessageBus bus) =>
        await bus.InvokeAsync<Todo>(new GetTodoByIdQuery { Id = id });

    internal static async Task Update(
        [AsParameters] UpdateTodoRequest request,
        [FromServices] IMessageBus bus) =>
        await bus.InvokeAsync(request.ToCommand());

    internal static async Task Complete(
        [AsParameters] CompleteTodoRequest request,
        [FromServices] IMessageBus bus) =>
        await bus.InvokeAsync(request.ToCommand());

    internal static async Task Remove(
        [FromRoute] Guid id,
        [FromServices] IMessageBus bus) =>
        await bus.InvokeAsync(new RemoveTodoCommand { Id = id });
}

public static class EndpointRouteBuilderExtensions
{
    private const string Pattern = "/{id:guid}";

    public static IEndpointRouteBuilder MapTodoEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/todos");
        group.MapGet("/", TodoEndpoints.Fetch);
        group.MapPost("/", TodoEndpoints.Create);
        group.MapGet(Pattern, TodoEndpoints.Get);
        group.MapPut(Pattern, TodoEndpoints.Update);
        group.MapPatch(Pattern, TodoEndpoints.Complete);
        group.MapDelete(Pattern, TodoEndpoints.Remove);
        return app;
    }
}

public class UpdateTodoRequest
{
    public class UpdateTodoRequestBody
    {
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
    }

    [FromRoute]
    public Guid Id { get; set; }

    [FromBody]
    public UpdateTodoRequestBody Body { get; set; } = new();

    public UpdateTodoCommand ToCommand() =>
        new() { Id = Id, Description = Body.Description, DueDate = Body.DueDate };
}

public class CompleteTodoRequest
{
    public class CompleteTodoRequestBody
    {
        public DateTime CompletedAt { get; set; }
    }

    [FromRoute]
    public Guid Id { get; set; }

    [FromBody]
    public CompleteTodoRequestBody Body { get; set; } = new();

    public CompleteTodoCommand ToCommand() =>
        new() { Id = Id, CompletedAt = Body.CompletedAt };
}
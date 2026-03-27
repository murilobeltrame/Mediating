using Application.Mediatr.TodoAggregate.Commands;
using Application.Mediatr.TodoAggregate.Queries;

using Domain.TodoAggregate;
using Domain.TodoAggregate.Specifications;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Rest.Mediatr.Endpoints;

public static class TodoEndpoints
{
    internal static async Task<IEnumerable<SimplerTodo>> Fetch(
        [AsParameters] FetchTodosByFilterQuery query,
        [FromServices] ISender sender) =>
        await sender.Send(query);

    internal static async Task<Todo> Create(
        [FromBody] CreateTodoCommand command,
        [FromServices] ISender sender) =>
        await sender.Send(command);

    internal static async Task<Todo> Get(
        [FromRoute] Guid id,
        [FromServices] ISender sender) =>
        await sender.Send(new GetTodoByIdQuery { Id = id });

    internal static async Task Update(
        [AsParameters] UpdateTodoRequest request,
        [FromServices] ISender sender) =>
        await sender.Send(request.ToCommand());

    internal static async Task Complete(
        [AsParameters] CompleteTodoRequest request,
        [FromServices] ISender sender) =>
        await sender.Send(request.ToCommand());

    internal static async Task Remove(
        [FromRoute] Guid id,
        [FromServices] ISender sender) =>
        await sender.Send(new RemoveTodoCommand { Id = id });
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
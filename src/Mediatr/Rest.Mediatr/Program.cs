using Application.Mediatr;

using Ardalis.Specification;

using Infrastructure.Db;

using Rest.Mediatr.Endpoints;
using Rest.Mediatr.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddAzureNpgsqlDbContext<ApplicationContext>("database");

builder.Services
    .AddTransient(typeof(IRepositoryBase<>), typeof(Repository<>))
    .AddMediatR(cfg =>
    {
        cfg.RegisterServicesFromAssemblyContaining<IApplication>();
        cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
    })
    .AddOpenApi();

var app = builder.Build();

app
    .MapTodoEndpoints()
    .MapOpenApi();

app
    .UseMiddleware<ExceptionHandlingMiddleware>()
    .UseHttpsRedirection();

await app.RunAsync();

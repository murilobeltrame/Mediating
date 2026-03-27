using Ardalis.Specification;

using Infrastructure.Db;

using Rest.Wolverine.Endpoints;

using Wolverine;
using Wolverine.FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddAzureNpgsqlDbContext<ApplicationContext>("database");

builder.Services
    .AddWolverine(o => o.UseFluentValidation())
    .AddTransient(typeof(IRepositoryBase<>), typeof(Repository<>))
    .AddOpenApi();

builder.UseWolverine(o => o.Durability.Mode = DurabilityMode.MediatorOnly);

var app = builder.Build();

app
    .MapTodoEndpoints()
    .MapOpenApi();

app.UseHttpsRedirection();

await app.RunAsync();

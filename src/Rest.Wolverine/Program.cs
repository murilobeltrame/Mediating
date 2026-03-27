using Application.Wolverine;

using Ardalis.Specification;

using Infrastructure.Db;

using JasperFx;

using Rest.Wolverine.Endpoints;

using Wolverine;
using Wolverine.FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddAzureNpgsqlDbContext<ApplicationContext>("database");

builder.Services
    .AddWolverine(o =>
    {
        //o.Discovery.IncludeAssembly(typeof(IApplication).Assembly);
        o.ApplicationAssembly = typeof(IApplication).Assembly;
        o.Durability.Mode = DurabilityMode.MediatorOnly;
        o.UseFluentValidation();
    })
    .AddTransient(typeof(IRepositoryBase<>), typeof(Repository<>))
    .AddOpenApi();

var app = builder.Build();

app
    .MapTodoEndpoints()
    .MapOpenApi();

app.UseHttpsRedirection();

await app.RunJasperFxCommands(args);

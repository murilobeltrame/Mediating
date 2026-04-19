using Acl.GeolocationService;

using Application.Mediatr;
using Application.Mediatr.Shared.PipelineBehaviours;

using Ardalis.Specification;

using AspNetShared;

using Domain;

using FluentValidation;

using Infrastructure.Db;

using Rest.Mediatr.Endpoints;
using Rest.Mediatr.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddAzureNpgsqlDbContext<ApplicationContext>("database");

builder.Services
    .AddTransient(typeof(IRepositoryBase<>), typeof(Repository<>))
    .AddScoped<ILocationService, LocationService>()
    .AddValidatorsFromAssembly(typeof(IDomain).Assembly)
    .AddEnrichersFromAssembly(typeof(IApplication).Assembly)
    .AddMediatR(cfg =>
    {
        cfg.RegisterServicesFromAssemblyContaining<IApplication>();
        // Order Matters, Enrichment should be before Validation,
        // otherwise validation will be executed before enrichment and it will fail because of missing data that should be added in enrichment.
        cfg.AddOpenBehavior(typeof(EnrichmentBehaviour<,>));
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

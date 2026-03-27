using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var database = builder.AddAzurePostgresFlexibleServer("database-server")
    .RunAsContainer(c => c
        .WithPgWeb()
        .WithDataVolume())
    .AddDatabase("database");

builder.AddProject<Rest_Mediatr>("api-mediatr")
    .WaitFor(database)
    .WithReference(database);

builder.AddProject<Rest_Wolverine>("api-wolverine")
    .WaitFor(database)
    .WithReference(database); ;

builder.AddProject<Migrations_Worker>("migrations-worker")
    .WaitFor(database)
    .WithReference(database);

await builder.Build().RunAsync();

using Microsoft.EntityFrameworkCore;

using Migrations.Worker;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.AddAzureNpgsqlDbContext<MigratonContext>("database");

var host = builder.Build();

await using var scope = host.Services.CreateAsyncScope();
var context = scope.ServiceProvider.GetRequiredService<MigratonContext>();
await context.Database.MigrateAsync();

await host.StartAsync();

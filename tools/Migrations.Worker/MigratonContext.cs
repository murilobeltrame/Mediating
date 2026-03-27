using Infrastructure.Db;

using Microsoft.EntityFrameworkCore;

namespace Migrations.Worker;

internal class MigratonContext(DbContextOptions options) : ApplicationContext(options);

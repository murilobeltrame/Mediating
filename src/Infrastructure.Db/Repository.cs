using Ardalis.Specification.EntityFrameworkCore;

namespace Infrastructure.Db;

public class Repository<T>(ApplicationContext context) : RepositoryBase<T>(context) where T : class;

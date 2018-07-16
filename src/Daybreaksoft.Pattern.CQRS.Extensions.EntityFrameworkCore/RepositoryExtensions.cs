using System;
using System.Linq;
using Daybreaksoft.Pattern.CQRS.DomainModel;

namespace Daybreaksoft.Pattern.CQRS.Extensions.EntityFrameworkCore
{
    public static class RepositoryExtensions
    {
        public static IQueryable<TEntity> GetQueryable<TEntity>(this IRepository<TEntity> repository) where TEntity : class ,IEntity, new()
        {
            if (repository is IDbContext context)
            {
                return context.Db.Set<TEntity>().AsQueryable();
            }
            else
            {
                throw new Exception($"The repository {repository.GetType().FullName} is not inherited from Daybreaksoft.Pattern.CQRS.Extensions.EntityFrameworkCore.IDbContext.");
            }
        }
    }
}

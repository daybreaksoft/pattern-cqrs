using System;
using System.Linq;
using System.Threading.Tasks;
using Daybreaksoft.Pattern.CQRS.DomainModel;

namespace Daybreaksoft.Pattern.CQRS.Extensions.EntityFrameworkCore
{
    public static class RepositoryExtensions
    {
        public static IQueryable<TEntity> GetQueryable<TEntity>(this IRepository<TEntity> repository) where TEntity : class ,IEntity
        {
            if (repository is IEfRepository context)
            {
                return context.Db.Set<TEntity>().AsQueryable();
            }
            else
            {
                throw new Exception($"The repository {repository.GetType().FullName} is not inherited from {typeof(IEfRepository).FullName}.");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Daybreaksoft.Pattern.CQRS.DomainModel
{
    public interface IRepositoryInvoker
    {
        Task<IEntity> FindAsync(object repository, object id);

        Task<IEntity> FindAsync(object repository, Type repositoryType, object id);

        Task<IEnumerable<TEntity>> FindAllAsync<TEntity>(object repository) where TEntity : IEntity;

        Task<IEnumerable<TEntity>> FindAllAsync<TEntity>(object repository, Type repositoryType) where TEntity : IEntity;

        Task InsertAsync(object repository, IEntity entity);

        Task InsertAsync(object repository, Type repositoryType, IEntity entity);

        Task UpdateAsync(object repository, IEntity entity);

        Task UpdateAsync(object repository, Type repositoryType, IEntity entity);

        Task RemoveAsync(object repository, object id);

        Task RemoveAsync(object repository, Type repositoryType, object id);
    }
}

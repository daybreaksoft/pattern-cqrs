using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Daybreaksoft.Pattern.CQRS.DomainModel
{
    public interface IRepositoryInvoker
    {
        Task<TEntity> FindAsync<TEntity>(object repository, object id) where TEntity : IEntity;

        Task<TEntity> FindAsync<TEntity>(object repository, Type repositoryType, object id) where TEntity : IEntity;

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

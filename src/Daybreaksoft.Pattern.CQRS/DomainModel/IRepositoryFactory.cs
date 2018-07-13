using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Daybreaksoft.Pattern.CQRS.DomainModel
{
    public interface IRepositoryFactory
    {
        Type GetRepositoryType(IEntity entity);

        Type GetRepositoryType(Type entityType);

        object GetRepository(IEntity entity);

        object GetRepository(Type entityType);

        Task<TEntity> InvokeFindAsync<TEntity>(object id) where TEntity : IEntity;

        Task<IEnumerable<TEntity>> InvokeFindAllAsync<TEntity>() where TEntity : IEntity;

        Task InvokeInsertAsync(IEntity entity);

        Task InvokeUpdateAsync(IEntity entity);

        Task InvokeRemoveAsync<TEntity>(object id) where TEntity : IEntity;

        Task InvokeRemoveAsync(Type entityType, object id);
    }
}

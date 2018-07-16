using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Daybreaksoft.Extensions.Functions;

namespace Daybreaksoft.Pattern.CQRS.DomainModel
{
    public class DefaultRepositoryInvoker : IRepositoryInvoker
    {
        protected readonly IDependencyInjection DI;

        public DefaultRepositoryInvoker(IDependencyInjection di)
        {
            DI = di;
        }

        public virtual Task<IEntity> FindAsync(object repository, object id)
        {
            return FindAsync(repository, repository.GetType(), id);
        }

        public async Task<IEntity> FindAsync(object repository, Type repositoryType, object id)
        {
            return await (Task<IEntity>)repositoryType.InvokeMethod("FindAsync", repository, id);
        }

        public virtual Task<IEnumerable<TEntity>> FindAllAsync<TEntity>(object repository) where TEntity : IEntity
        {
            return FindAllAsync<TEntity>(repository, repository.GetType());
        }

        public async Task<IEnumerable<TEntity>> FindAllAsync<TEntity>(object repository, Type repositoryType) where TEntity : IEntity
        {
            return await (Task<IEnumerable<TEntity>>)repositoryType.InvokeMethod("FindAllAsync", repository);
        }

        public virtual Task InsertAsync(object repository, IEntity entity)
        {
            return InsertAsync(repository, repository.GetType(), entity);
        }

        public async Task InsertAsync(object repository, Type repositoryType, IEntity entity)
        {
            await (Task)repositoryType.InvokeMethod("InsertAsync", repository, entity);
        }

        public virtual Task UpdateAsync(object repository, IEntity entity)
        {
            return UpdateAsync(repository, repository.GetType(), entity);
        }

        public async Task UpdateAsync(object repository, Type repositoryType, IEntity entity)
        {
            await(Task)repositoryType.InvokeMethod("UpdateAsync", repository, entity);
        }

        public virtual Task RemoveAsync(object repository, object id)
        {
            return RemoveAsync(repository, repository.GetType(), id);
        }

        public async Task RemoveAsync(object repository, Type repositoryType, object id)
        {
            await (Task)repositoryType.InvokeMethod("DeleteAsync", repository, id);
        }
    }
}

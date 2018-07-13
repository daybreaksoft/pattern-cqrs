using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Daybreaksoft.Extensions.Functions;

namespace Daybreaksoft.Pattern.CQRS.DomainModel
{
    public class DefaultRepositoryFactory : IRepositoryFactory
    {
        protected readonly IDependencyInjection DI;

        public DefaultRepositoryFactory(IDependencyInjection di)
        {
            DI = di;
        }

        #region Get Repository

        public virtual Type GetRepositoryType(IEntity entity)
        {
            return GetRepositoryType(entity.GetType());
        }

        public virtual Type GetRepositoryType(Type entityType)
        {
            return typeof(IRepository<>).MakeGenericType(entityType);
        }

        public virtual object GetRepository(IEntity entity)
        {
            var type = GetRepositoryType(entity);

            return GetRepository(type);
        }

        public virtual object GetRepository(Type entityType)
        {
            return DI.GetService(entityType);
        }

        #endregion

        #region Invoke Repository Methods

        public virtual async Task<TEntity> InvokeFindAsync<TEntity>(object id) where TEntity : IEntity
        {
            var type = GetRepositoryType(typeof(TEntity));
            var respository = GetRepository(type);

            return await(Task<TEntity>)type.InvokeMethod("FindAsync", respository, id);
        }

        public virtual async Task<IEnumerable<TEntity>> InvokeFindAllAsync<TEntity>() where TEntity : IEntity
        {
            var type = GetRepositoryType(typeof(TEntity));
            var respository = GetRepository(type);

            return await(Task<IEnumerable<TEntity>>)type.InvokeMethod("FindAllAsync", respository);
        }

        public virtual async Task InvokeInsertAsync(IEntity entity)
        {
            var type = GetRepositoryType(entity);
            var respository = GetRepository(type);

            await(Task)type.InvokeMethod("InsertAsync", respository, entity);
        }

        public virtual async Task InvokeUpdateAsync(IEntity entity)
        {
            var type = GetRepositoryType(entity);
            var respository = GetRepository(type);

            await(Task)type.InvokeMethod("UpdateAsync", respository, entity);
        }

        public virtual async Task InvokeRemoveAsync<TEntity>(object id) where TEntity : IEntity
        {
            var type = GetRepositoryType(typeof(TEntity));
            var respository = GetRepository(type);

            await(Task)type.InvokeMethod("RemoveAsync", respository, id);
        }

        public virtual async Task InvokeRemoveAsync(Type entityType, object id)
        {
            var type = GetRepositoryType(entityType);
            var respository = GetRepository(type);

            await(Task)type.InvokeMethod("RemoveAsync", respository, id);
        }

        #endregion
    }
}

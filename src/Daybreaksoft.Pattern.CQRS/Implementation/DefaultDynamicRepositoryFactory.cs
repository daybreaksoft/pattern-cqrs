﻿using System;
using System.Data;
using System.Threading.Tasks;
using Daybreaksoft.Extensions.Functions;

namespace Daybreaksoft.Pattern.CQRS.Implementation
{
    public class DefaultDynamicRepositoryFactory : IDynamicRepositoryFactory
    {
        protected readonly IDependencyInjection DI;

        public DefaultDynamicRepositoryFactory(IDependencyInjection di)
        {
            DI = di;
        }

        public object GetRepository(IAggregateRoot aggregate)
        {
            var type = GetRepositoryType(aggregate);

            return GetRepository(type);
        }

        public object GetRepository(Type type)
        {
            return DI.GetService(type);
        }

        public virtual Type GetRepositoryType(IAggregateRoot aggregate)
        {
            return GetRepositoryType(aggregate.GetType());
        }

        public virtual Type GetRepositoryType(Type aggregateType)
        {
            return typeof(IRepository<>).MakeGenericType(aggregateType);
        }

        #region Invoke Methods

        public async Task InvokeInsertAsync(IAggregateRoot aggregate, IDbTransaction transaction = null)
        {
            var type = GetRepositoryType(aggregate);
            var respository = GetRepository(type);

            await (Task)type.InvokeMethod("InsertAsync", respository, aggregate, transaction);
        }

        public async Task InvokeUpdateAsync(IAggregateRoot aggregate, IDbTransaction transaction = null)
        {
            var type = GetRepositoryType(aggregate);
            var respository = GetRepository(type);

            await (Task)type.InvokeMethod("UpdateAsync", respository, aggregate, transaction);
        }

        public async Task InvokeRemoveAsync<TAggregateRoot>(object id, IDbTransaction transaction = null) where TAggregateRoot : IAggregateRoot
        {
            var type = GetRepositoryType(typeof(TAggregateRoot));
            var respository = GetRepository(type);

            await (Task)type.InvokeMethod("RemoveAsync", respository, id, transaction);
        }

        public async Task InvokeRemoveAsync(Type aggregateType, object id, IDbTransaction transaction = null)
        {
            var type = GetRepositoryType(aggregateType);
            var respository = GetRepository(type);

            await(Task)type.InvokeMethod("RemoveAsync", respository, id, transaction);
        }

        public async Task<TAggregateRoot> InvokeFindAsync<TAggregateRoot>(object id, IDbTransaction transaction = null) where TAggregateRoot : IAggregateRoot
        {
            var type = GetRepositoryType(typeof(TAggregateRoot));
            var respository = GetRepository(type);

            return await (Task<TAggregateRoot>)type.InvokeMethod("FindAsync", respository, id, transaction);
        }

        #endregion
    }
}

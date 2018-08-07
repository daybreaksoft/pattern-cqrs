using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Daybreaksoft.Extensions.Functions;

namespace Daybreaksoft.Pattern.CQRS.DomainModel
{
    public abstract class AbstractApplicationService<TAggregateRoot, TEntity> : IApplicationService<TAggregateRoot>
        where TAggregateRoot : IAggregateRoot
        where TEntity : class, IEntity, new()
    {
        protected readonly IRepository<TEntity> Repository;

        protected AbstractApplicationService(IRepository<TEntity> repository)
        {
            Repository = repository;
        }

        public virtual async Task<TAggregateRoot> FindAsync(object id)
        {
            return ConvertToAggregate(await Repository.FindAsync(id));
        }

        public virtual async Task<IEnumerable<TAggregateRoot>> FindAllAsync()
        {
            var collection = await Repository.FindAllAsync();

            return collection.Select(ConvertToAggregate);
        }

        public virtual async Task InsertAsync(TAggregateRoot aggregate, bool immediate = false)
        {
            await BeforeInsertAsync(aggregate);

            var newEntity = new TEntity();

            CopyValueToEntity(newEntity, aggregate);

            await Repository.InsertAsync(newEntity, (entity) =>
            {

                // Get id of entity after added
                var keyProperty = entity.GetType().FindProperty<KeyAttribute>();

                // Set id value for aggregate
                if (aggregate is IAggregateRootSetKey key)
                {
                    key.SetKey(keyProperty.GetValue(entity));
                }
                else
                {
                    throw new Exception($"The {aggregate.GetType().FullName} is not inherited from {typeof(IAggregateRootSetKey).FullName}.");
                }
            }, immediate);
        }

        public virtual async Task UpdateAsync(TAggregateRoot aggregate, bool immediate = false)
        {
            await BeforeUpdateAsync(aggregate);

            var entity = new TEntity();

            CopyValueToEntity(entity, aggregate);

            await Repository.UpdateAsync(entity, immediate);
        }

        public virtual async Task DeleteAsync(object id, bool immediate = false)
        {
            await BeforeDeleteAsync(id);

            await Repository.DeleteAsync(id, immediate);
        }

        protected virtual async Task BeforeInsertAsync(TAggregateRoot aggregate)
        {
#if !Net451
            await Task.CompletedTask;
#else
            await Task.FromResult(0);
#endif
        }

        protected virtual async Task BeforeUpdateAsync(TAggregateRoot aggregate)
        {
#if !Net451
            await Task.CompletedTask;
#else
            await Task.FromResult(0);
#endif
        }

        protected virtual async Task BeforeDeleteAsync(object id)
        {
#if !Net451
            await Task.CompletedTask;
#else
            await Task.FromResult(0);
#endif
        }

        #region Data Transfer

        protected abstract void CopyValueToEntity(TEntity entity, TAggregateRoot aggregate);

        protected abstract TAggregateRoot ConvertToAggregate(TEntity entity);

        #endregion
    }
}

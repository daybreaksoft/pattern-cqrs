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

        public virtual async Task InsertAsync(TAggregateRoot aggregate)
        {
            var newEntity = new TEntity();

            CopyValueToEntity(newEntity, aggregate);

            await Repository.InsertAsync(newEntity);

            // Get id of entity after added
            var keyProperty = newEntity.GetType().FindProperty<KeyAttribute>();
            var id = keyProperty.GetValue(newEntity);

            // Set id value for aggregate
            if (aggregate is IAggregateRootSetKey key)
            {
                key.SetKey(id);
            }
            else
            {
                throw  new Exception($"The {aggregate.GetType().FullName} does not inherit {typeof(IAggregateRootSetKey).FullName}.");
            }
        }

        public virtual async Task UpdateAsync(TAggregateRoot aggregate)
        {
            var unModifiedEntity = await Repository.FindAsync(aggregate.Id);

            CopyValueToEntity(unModifiedEntity, aggregate);

            await Repository.UpdateAsync(unModifiedEntity);
        }

        public virtual Task DeleteAsync(object id)
        {
            return Repository.DeleteAsync(id);
        }

        #region Data Transfer

        protected abstract void CopyValueToEntity(TEntity entity, TAggregateRoot aggregate);

        protected abstract TAggregateRoot ConvertToAggregate(TEntity entity);

        #endregion
    }
}

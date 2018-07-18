using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Daybreaksoft.Pattern.CQRS.DomainModel
{
    public abstract class AbstractDomainAppService<TAggregateRoot, TEntity> : IDomainService<TAggregateRoot>
        where TAggregateRoot : IAggregateRoot
        where TEntity : class, IEntity, new()
    {
        protected readonly IRepository<TEntity> Repository;

        protected AbstractDomainAppService(IRepository<TEntity> repository)
        {
            Repository = repository;
        }

        public virtual async Task<TAggregateRoot> FindAsync(object id)
        {
            return ConvertToAggregate(await Repository.FindAsync(id));
        }

        public virtual Task<IEnumerable<TAggregateRoot>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public virtual Task InsertAsync(TAggregateRoot aggregate)
        {
            var newEntity = new TEntity();

            CopyValueToEntity(newEntity, aggregate);

            return Repository.InsertAsync(newEntity);
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

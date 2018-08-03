using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Daybreaksoft.Extensions.Functions;
using Daybreaksoft.Pattern.CQRS.Extensions;

namespace Daybreaksoft.Pattern.CQRS.DomainModel
{
    public class SimpleDomainService<TAggregateRoot, TEntity> : AbstractDomainService<TAggregateRoot, TEntity>
        where TAggregateRoot : IAggregateRoot, IEntity
        where TEntity : class, IEntity, new()
    {
        public SimpleDomainService(IRepository<TEntity> repository) : base(repository)
        {
        }

        protected override void CopyValueToEntity(TEntity entity, TAggregateRoot aggregate)
        {
            aggregate.CopyValueTo(entity);
        }

        protected override TAggregateRoot ConvertToAggregate(TEntity entity)
        {
            // Try to create aggregate instance
            var instance = InstanceHelper.CreateInstance(typeof(TAggregateRoot));

            // Copy all values from entity to aggregate instance
            entity.CopyValueTo(instance);

            return (TAggregateRoot)instance;
        }
    }

    public class SimpleDomainService<TAggregateRoot> : IDomainService<TAggregateRoot>
        where TAggregateRoot : IAggregateRoot, IEntity
    {
        protected readonly IRepository Repository;

        public SimpleDomainService(IRepositoryFactory repositoryFactory)
        {
            Repository = GetRepository(repositoryFactory);
        }

        private IRepository GetRepository(IRepositoryFactory repositoryFactory)
        {
            try
            {
                var aggregateType = typeof(TAggregateRoot);

#if !NetStandard13
                var entityType = aggregateType.BaseType;
#else
                var entityType = aggregateType.GetTypeInfo().BaseType;
#endif
                var repositoryType = repositoryFactory.GetRepositoryType(entityType);

                return (IRepository)repositoryFactory.GetRepository(repositoryType);
            }
            catch (Exception e)
            {
                throw new Exception($"Try to get repository with {typeof(TAggregateRoot).FullName} failed. {e.Message}");
            }
        }

        public virtual async Task<TAggregateRoot> FindAsync(object id)
        {
            var entity = await Repository.FindAsync(id);

            return ConvertToAggregate(entity);
        }

        public virtual async Task<IEnumerable<TAggregateRoot>> FindAllAsync()
        {
            var entities = await Repository.FindAllAsync();

            var aggregates = new List<TAggregateRoot>();
            foreach (var entity in (IEnumerable)entities)
            {
                aggregates.Add(ConvertToAggregate(entity));
            }

            return aggregates;
        }

        public virtual Task InsertAsync(TAggregateRoot aggregate)
        {
            return Repository.InsertAsync(aggregate);
        }

        public virtual Task UpdateAsync(TAggregateRoot aggregate)
        {
            return Repository.UpdateAsync(aggregate);
        }

        public virtual Task DeleteAsync(object id)
        {
            return Repository.DeleteAsync(id);
        }

        #region Data Transfer

        protected virtual TAggregateRoot ConvertToAggregate(object entity)
        {
            // Try to create aggregate instance
            var instance = InstanceHelper.CreateInstance(typeof(TAggregateRoot));

            // Copy all values from entity to aggregate instance
            entity.CopyValueTo(instance);

            return (TAggregateRoot)instance;
        }

        #endregion
    }
}

using System;
using System.Data;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using Daybreaksoft.Extensions.Functions;

namespace Daybreaksoft.Pattern.CQRS.Extensions.Dapper
{
    /// <summary>
    /// Default implemention of IQuery with EntityFrameworkCore
    /// </summary>
    public abstract class AbstractRepository<TAggregate, TEntity> : IRepository<TAggregate>
        where TAggregate : IAggregateRoot
        where TEntity : class, IEntity, new()
    {
        protected readonly IDbConnection Connection;
        protected readonly IAggregateBus Aggregates;

        protected AbstractRepository(IDbConnection connection, IAggregateBus aggregateBus)
        {
            Connection = connection;
            Aggregates = aggregateBus;
        }

        /// <summary>
        /// Find an entity by id
        /// </summary>
        public virtual async Task<TAggregate> FindAsync(object id, IDbTransaction transaction = null)
        {
            return ConvertToAggregate(await Connection.GetAsync<TEntity>(id, transaction));
        }

        /// <summary>
        /// Insert an entity
        /// </summary>
        public virtual async Task InsertAsync(TAggregate aggregate, IDbTransaction transaction = null)
        {
            if (aggregate == null) throw new ArgumentNullException(nameof(aggregate));

            await Connection.InsertAsync(ConvertToEntity(aggregate), transaction);
        }

        /// <summary>
        /// Update an entity
        /// </summary>
        public virtual async Task UpdateAsync(TAggregate aggregate, IDbTransaction transaction = null)
        {
            if (aggregate == null) throw new ArgumentNullException(nameof(aggregate));

            await Connection.UpdateAsync(ConvertToEntity(aggregate), transaction);
        }

        /// <summary>
        /// Remove an entity by key
        /// </summary>
        public virtual async Task RemoveAsync(object id, IDbTransaction transaction = null)
        {
            var entity = new TEntity();

            // Find key property and set id value to this property.
            var entityType = typeof(TEntity);
            var keyProperty = entityType.FindProperty<System.ComponentModel.DataAnnotations.KeyAttribute>();
            
            if (keyProperty == null) throw new NullReferenceException($"Cannot found the property which has System.ComponentModel.DataAnnotations.KeyAttribute within entity type {entityType.Name}");

            keyProperty.SetValue(entity, id);

            await Connection.DeleteAsync(entity, transaction);
        }

        protected virtual TEntity ConvertToEntity(TAggregate aggregate)
        {
            var entity = new TEntity();

            aggregate.CopyValueTo(entity, forcePropertyNames: new[] { "Id" });

            return entity;
        }

        protected virtual TAggregate ConvertToAggregate(TEntity entity)
        {
            var aggregate = Aggregates.BuildAggregate<TAggregate>();

            entity.CopyValueTo(aggregate);

            return aggregate;
        }
    }
}

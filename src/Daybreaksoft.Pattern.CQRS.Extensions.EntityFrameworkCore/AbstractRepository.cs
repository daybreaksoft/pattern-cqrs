using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Daybreaksoft.Extensions.Functions;
using Microsoft.EntityFrameworkCore;

namespace Daybreaksoft.Pattern.CQRS.Extensions.EntityFrameworkCore
{
    /// <summary>
    /// Default implemention of IQuery with EntityFrameworkCore
    /// </summary>
    public abstract class AbstractRepository<TAggregate, TEntity> : IRepository<TAggregate> 
        where TAggregate : IAggregateRoot
        where TEntity : class, IEntity, new()
    {
        protected readonly DbContext Db;
        protected readonly IAggregateBus Aggregates;

        public AbstractRepository(DbContext db, IAggregateBus aggregateBus)
        {
            Db = db;
            Aggregates = aggregateBus;
        }

        /// <summary>
        /// Find an entity by id
        /// </summary>
        public virtual async Task<TAggregate> FindAsync(object id)
        {
            return ConvertToAggregate(await Db.Set<TEntity>().FindAsync(id));
        }

        /// <summary>
        /// Insert an entity
        /// </summary>
        public virtual async Task InsertAsync(TAggregate aggregate)
        {
            if (aggregate == null) throw new ArgumentNullException(nameof(aggregate));

            // Insert entity
            await Db.Set<TEntity>().AddAsync(ConvertToEntity(aggregate));

            // Submit changes
            await Db.SaveChangesAsync();
        }

        /// <summary>
        /// Update an entity
        /// </summary>
        public virtual async Task UpdateAsync(TAggregate aggregate)
        {
            if (aggregate == null) throw new ArgumentNullException(nameof(aggregate));

            var entity = await Db.Set<TEntity>().FindAsync(aggregate.Id);

            aggregate.CopyValueTo(entity);

            // Submit changes
            await Db.SaveChangesAsync();
        }

        /// <summary>
        /// Remove an entity by key
        /// </summary>
        public virtual async Task RemoveAsync(object id)
        {
            // Generate new entity
            var entity = new TEntity();

            // Find key property
            var keyProperty = typeof(TEntity).FindProperty<KeyAttribute>();

            // Set key property value
            keyProperty.SetValue(entity, id);

            // Change entity state to delted
            var entityEntry = Db.Entry(entity);
            entityEntry.State = EntityState.Deleted;

            // Submit changes
            await Db.SaveChangesAsync();
        }

        protected virtual TEntity ConvertToEntity(TAggregate aggregate)
        {
            var entity = new TEntity();

            aggregate.CopyValueTo(entity);

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

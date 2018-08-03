using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Daybreaksoft.Extensions.Functions;
using Daybreaksoft.Pattern.CQRS.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace Daybreaksoft.Pattern.CQRS.Extensions.EntityFrameworkCore
{
    /// <summary>
    /// Default implemention of IRepository with EntityFrameworkCore
    /// </summary>
    public class DefaultRepository<TEntity> : IRepository<TEntity>, IDbContext
        where TEntity : class, IEntity
    {
        public DefaultRepository(DbContext db)
        {
            Db = db;
        }

        public DbContext Db { get; }

        /// <summary>
        /// Find an entity via id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TEntity> FindAsync(object id)
        {
            return await Db.Set<TEntity>().FindAsync(id);
        }

        /// <summary>
        /// Find all entities
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> FindAllAsync()
        {
            return await Task.FromResult(Db.Set<TEntity>().AsEnumerable());
        }

        /// <summary>
        /// Insert an entity.
        /// </summary>
        /// <param name="entity">The entity instance.</param>
        /// <returns></returns>
        public virtual async Task InsertAsync(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            await Db.Set<TEntity>().AddAsync(entity);

            await Db.SaveChangesAsync();
        }

        /// <summary>
        /// Update an entity.
        /// </summary>
        /// <param name="entity">The entity instance.</param>
        /// <returns></returns>
        public virtual async Task UpdateAsync(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            // Try to find key property
            var keyProperty = entity.GetType().FindProperty<KeyAttribute>();
            if (keyProperty == null)throw new Exception($"Cannot found key property in the {entity.GetType().FullName}.");

            // Get unmodified entity from database
            var unmodifiedEntity = await FindAsync(keyProperty.GetValue(entity));

            // Copy values from entity to unmodified entity
            entity.CopyValueTo(unmodifiedEntity);

            await Db.SaveChangesAsync();
        }

        /// <summary>
        /// Delete an entity.
        /// </summary>
        /// <param name="key">The key of the entity.</param>
        /// <returns></returns>
        public async Task DeleteAsync(object key)
        {
            var entity = await FindAsync(key);

            Db.Set<TEntity>().Remove(entity);

            await Db.SaveChangesAsync();
        }

        async Task<object> IRepository.FindAsync(object id)
        {
            return await this.FindAsync(id);
        }

        async Task<object> IRepository.FindAllAsync()
        {
            return await this.FindAllAsync();
        }

        async Task IRepository.InsertAsync(object entity)
        {
            await InsertAsync((TEntity)entity);
        }

        async Task IRepository.UpdateAsync(object entity)
        {
            await this.UpdateAsync((TEntity)entity);
        }
    }
}

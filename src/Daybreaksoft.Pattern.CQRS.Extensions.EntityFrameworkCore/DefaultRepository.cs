using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Daybreaksoft.Extensions.Functions;
using Microsoft.EntityFrameworkCore;

namespace Daybreaksoft.Pattern.CQRS.Extensions.EntityFrameworkCore
{
    /// <summary>
    /// Default repository with EntityFrameworkCore
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DefaultRepository<T> : IRepository<T> where T : class, IEntity, new()
    {
        protected readonly IDbContext Db;

        public DefaultRepository(IDbContext db)
        {
            Db = db;
        }

        /// <summary>
        /// Find an entity by id
        /// </summary>
        public Task<T> FindAsync(object id)
        {
            return Db.Set<T>().FindAsync(id);
        }

        /// <summary>
        /// Find all entities
        /// </summary>
        public Task<List<T>> FindAllAsync()
        {
            return Db.Set<T>().ToListAsync();
        }

        /// <summary>
        /// Insert an entity
        /// </summary>
        public async Task InsertAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            // Insert entity
            await Db.Set<T>().AddAsync(entity);

            // Submit changes
            await Db.SaveChangesAsync();
        }

        /// <summary>
        /// Update an entity
        /// </summary>
        public async Task UpdateAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            // Change entity state to modified
            var entityEntry = Db.Entry(entity);
            entityEntry.State = EntityState.Modified;

            // Submit changes
            await Db.SaveChangesAsync();
        }

        /// <summary>
        /// Remove an entity by key
        /// </summary>
        public async Task DeleteAsync(object id)
        {
            // Generate new entity
            var entity = new T();

            // Find key property
            var keyProperty = typeof(T).FindProperty<KeyAttribute>();

            // Set key property value
            keyProperty.SetValue(entity, id);

            // Change entity state to delted
            var entityEntry = Db.Entry(entity);
            entityEntry.State = EntityState.Deleted;

            // Submit changes
            await Db.SaveChangesAsync();
        }
    }
}

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
        public Task<IEnumerable<TEntity>> FindAllAsync()
        {
            throw new NotSupportedException();
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

            Db.Entry(entity).State = EntityState.Modified;

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
    }
}

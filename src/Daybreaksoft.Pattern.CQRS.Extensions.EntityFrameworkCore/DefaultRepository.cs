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
    public class DefaultRepository<TEntity> : IRepository<TEntity>, IEfRepository
        where TEntity : class, IEntity, new()
    {
        protected readonly IUnitOfWork UnitOfWork;

        public DefaultRepository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;

            if (UnitOfWork is IEfUnitOfWork efUnitOfWork)
            {
                Db = efUnitOfWork.DbContext;
            }
            else
            {
                throw new Exception($"The {unitOfWork.GetType().FullName} is not inherited from {typeof(IEfUnitOfWork).FullName}.");
            }
        }

        public DbContext Db { get; private set; }

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
        public virtual async Task InsertAsync(TEntity entity, Action<IEntity> setKeyAction, bool immediate = false)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            await UnitOfWork.RegisterAddedAsync(entity, this, setKeyAction);

            if (immediate)
            {
                await UnitOfWork.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Update an entity.
        /// </summary>
        /// <param name="entity">The entity instance.</param>
        /// <returns></returns>
        public virtual async Task UpdateAsync(TEntity entity, bool immediate = false)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            await UnitOfWork.RegisterChangedAsync(entity, this);

            if (immediate)
            {
                await UnitOfWork.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Delete an entity.
        /// </summary>
        /// <param name="key">The key of the entity.</param>
        /// <returns></returns>
        public async Task DeleteAsync(object key, bool immediate = false)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));

            var entity = new TEntity();

            // Try to find key property
            var keyProperty = entity.GetType().FindProperty<KeyAttribute>();
            if (keyProperty == null) throw new Exception($"Cannot found key property in the {entity.GetType().FullName}.");

            // Set id
            keyProperty.SetValue(entity, key);

            await UnitOfWork.RegisterRemovedAsync(entity, this);

            if (immediate)
            {
                await UnitOfWork.SaveChangesAsync();
            }
        }

        async Task<object> IRepository.FindAsync(object id)
        {
            return await this.FindAsync(id);
        }

        async Task<object> IRepository.FindAllAsync()
        {
            return await this.FindAllAsync();
        }

        async Task IRepository.InsertAsync(object entity, Action<IEntity> setKeyAction, bool immediate)
        {
            await InsertAsync((TEntity)entity, setKeyAction, immediate);
        }

        async Task IRepository.UpdateAsync(object entity, bool immediate)
        {
            await this.UpdateAsync((TEntity)entity, immediate);
        }

        public Task PersistInsertOf(object entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            return Db.Set<TEntity>().AddAsync((TEntity)entity);
        }

        public async Task PersistUpdateOf(object entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            // Try to find key property
            var keyProperty = entity.GetType().FindProperty<KeyAttribute>();
            if (keyProperty == null) throw new Exception($"Cannot found key property in the {entity.GetType().FullName}.");

            // Get unmodified entity from database
            var unmodifiedEntity = await FindAsync(keyProperty.GetValue(entity));

            // Copy values from entity to unmodified entity
            entity.CopyValueTo(unmodifiedEntity);
        }

        public async Task PersistDeleteOf(object entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            Db.Set<TEntity>().Remove((TEntity)entity);

            await Task.CompletedTask;
        }
    }
}

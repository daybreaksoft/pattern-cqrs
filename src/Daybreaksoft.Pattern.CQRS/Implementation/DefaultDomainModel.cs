using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Daybreaksoft.Extensions.Functions;

namespace Daybreaksoft.Pattern.CQRS
{
    /// <summary>
    /// Default implemention of IDomainModel
    /// </summary>
    public abstract class DefaultDomainModel<TEntity> : IDomainModel
        where TEntity : IEntity, new()
    {
        protected readonly IRepository<TEntity> Repository;

        protected DefaultDomainModel(IRepository<TEntity> repository)
        {
            Repository = repository;
        }

        /// <summary>
        /// The key of the model
        /// </summary>
        [Key]
        public virtual object Id { get; set; }

        /// <summary>
        /// Load object
        /// </summary>
        /// <exception cref="ArgumentNullException">The key of Model cannot be null.</exception>
        /// <exception cref="KeyNotFoundException">Cannot load model with key.</exception>
        public virtual async Task LoadAsync()
        {
            if(Id == null) throw new ArgumentNullException($"The key of {this.GetType().Name} cannot be null.");

            var entity = await Repository.FindAsync(Id);

            if(entity == null) throw new KeyNotFoundException($"Cannot load {this.GetType().Name} with key {Id}");

            entity.CopyValueTo(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual async Task AddAsync()
        {
            var entity = new TEntity();

            this.CopyValueTo(entity);

            await Repository.InsertAsync(entity);
        }

        /// <summary>
        /// Update object
        /// </summary>
        public virtual async Task UpdateAsync()
        {
            var entity = await Repository.FindAsync(Id);

            this.CopyValueTo(entity);

            await Repository.UpdateAsync(entity);
        }

        /// <summary>
        /// Remove object
        /// </summary>
        public virtual async Task RemoveAsync()
        {
            await Repository.DeleteAsync(Id);
        }
    }
}

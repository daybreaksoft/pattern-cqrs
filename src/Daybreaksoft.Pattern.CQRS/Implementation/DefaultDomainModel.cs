using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Daybreaksoft.Extensions.Functions;

namespace Daybreaksoft.Pattern.CQRS
{
    /// <summary>
    /// Domain model
    /// </summary>
    public abstract class DefaultDomainModel<TEntity> : IDomainModel
        where TEntity : IEntity, new()
    {
        protected readonly IRepository<TEntity> Repository;

        protected DefaultDomainModel(IRepository<TEntity> repository)
        {
            Repository = repository;
        }

        protected DefaultDomainModel(object id, IRepository<TEntity> repository)
        {
            Id = id;
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
        public virtual async Task LoadAsync()
        {
            var entity = await Repository.FindAsync(Id);

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

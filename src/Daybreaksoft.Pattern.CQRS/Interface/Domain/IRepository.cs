using System.Collections.Generic;
using System.Threading.Tasks;

namespace Daybreaksoft.Pattern.CQRS
{
    /// <summary>
    /// Repository that is a data access layer.
    /// </summary>
    public interface IRepository<TEntity>
        where TEntity : IEntity
    {
        /// <summary>
        /// Find an entity by id
        /// </summary>
        Task<TEntity> FindAsync(object id);

        /// <summary>
        /// Find all entities
        /// </summary>
        Task<List<TEntity>> FindAllAsync();

        /// <summary>
        /// Insert an new entity
        /// </summary>
        Task InsertAsync(TEntity entity);

        /// <summary>
        /// Update an entity
        /// </summary>
        Task UpdateAsync(TEntity entity);

        /// <summary>
        /// Delete an entity by id
        /// </summary>
        Task DeleteAsync(object id);
    }
}

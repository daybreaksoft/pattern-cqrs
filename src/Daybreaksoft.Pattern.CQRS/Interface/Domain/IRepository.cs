using System.Collections.Generic;
using System.Threading.Tasks;

namespace Daybreaksoft.Pattern.CQRS
{
    /// <summary>
    /// Repository that is a data access layer.
    /// </summary>
    public interface IRepository<TAggregateRoot>
        where TAggregateRoot : IAggregateRoot
    {
        /// <summary>
        /// Find an entity by id
        /// </summary>
        Task<TAggregateRoot> FindAsync(object id);

        /// <summary>
        /// Find all entities
        /// </summary>
        Task<List<TAggregateRoot>> FindAllAsync();

        /// <summary>
        /// Insert an new entity
        /// </summary>
        Task InsertAsync(TAggregateRoot entity);

        /// <summary>
        /// Update an entity
        /// </summary>
        Task UpdateAsync(TAggregateRoot entity);

        /// <summary>
        /// Delete an entity by id
        /// </summary>
        Task DeleteAsync(object id);
    }
}

using System.Collections.Generic;
using System.Data;
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
        Task<TAggregateRoot> FindAsync(object id, IDbTransaction transaction = null);

        /// <summary>
        /// Insert an new aggreagate
        /// </summary>
        Task InsertAsync(TAggregateRoot aggreagate, IDbTransaction transaction = null);

        /// <summary>
        /// Update an aggreagate
        /// </summary>
        Task UpdateAsync(TAggregateRoot aggreagate, IDbTransaction transaction = null);

        /// <summary>
        /// Delete an aggreagate by id
        /// </summary>
        Task RemoveAsync(object id, IDbTransaction transaction = null);
    }
}

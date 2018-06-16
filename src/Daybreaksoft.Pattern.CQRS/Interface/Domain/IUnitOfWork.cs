using System.Threading.Tasks;

namespace Daybreaksoft.Pattern.CQRS
{
    /// <summary>
    /// Unit of work
    /// </summary>
    public interface IUnitOfWork
    {
        Task OpenAsync();

        Task CommitAsync();

        /// <summary>
        /// Build an empte aggreate from current unit of work.
        /// </summary>
        /// <typeparam name="TAggregateRoot"></typeparam>
        /// <param name="addToUnCommitted"></param>
        /// <returns>Empty aggreate</returns>
        TAggregateRoot BuildAggregate<TAggregateRoot>(bool addToUnCommitted = true) where TAggregateRoot : IAggregateRoot, new();

        Task<TAggregateRoot> GetAggregate<TAggregateRoot>(object id, bool addToUnCommitted = true) where TAggregateRoot : IAggregateRoot, new();
    }
}

using System;
using System.Threading.Tasks;

namespace Daybreaksoft.Pattern.CQRS.DomainModel
{
    /// <summary>
    /// UnitOfWork接口
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        Task BeginAsync();

        Task CommitAsync();

        IDomainService<TAggregate> DomainService<TAggregate>() where TAggregate : IAggregateRoot;

        Task AddToStorageAsync<TAggregate>(TAggregate aggregate) where TAggregate : IAggregateRoot;

        Task ModifyWithinStorageAsync<TAggregate>(TAggregate aggregate) where TAggregate : IAggregateRoot;

        Task RemoveFromStorageAsync<TAggregate>(object id) where TAggregate : IAggregateRoot;
    }
}

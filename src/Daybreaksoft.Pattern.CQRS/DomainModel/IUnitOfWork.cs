using System;
using System.Threading.Tasks;

namespace Daybreaksoft.Pattern.CQRS.DomainModel
{
    /// <summary>
    /// UnitOfWork接口
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        Task SaveChangesAsync();

        Task CommitAsync();

        Task RegisterAddedAsync(IEntity entity, IRepository repository, Action<IEntity> setKeyAction);

        Task RegisterChangedAsync(IEntity entity, IRepository repository);

        Task RegisterRemovedAsync(IEntity entity, IRepository repository);
    }
}

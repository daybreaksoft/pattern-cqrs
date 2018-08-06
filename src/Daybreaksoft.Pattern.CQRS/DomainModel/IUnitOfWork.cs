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
    }
}

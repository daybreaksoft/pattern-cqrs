using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Daybreaksoft.Pattern.CQRS.DomainModel
{
    /// <summary>
    /// UnitOfWork接口
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        ICollection<RegisterdModel> RegisterdModels { get; }

        Task BeginAsync();

        Task CommitAsync();
    }

    public enum RegisterAction
    {
        Add,
        Modify,
        Delete
    }

    public class RegisterdModel
    {
        public IAggregateRoot Model { get; set; }

        public RegisterAction Action { get; set; }

        public IApplicationPersistService PersistService { get; set; }
    }
}

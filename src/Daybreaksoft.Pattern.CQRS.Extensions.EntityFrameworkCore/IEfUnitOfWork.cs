using Daybreaksoft.Pattern.CQRS.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace Daybreaksoft.Pattern.CQRS.Extensions.EntityFrameworkCore
{
    public interface IEfUnitOfWork : IUnitOfWork
    {
        DbContext DbContext { get; }
    }
}

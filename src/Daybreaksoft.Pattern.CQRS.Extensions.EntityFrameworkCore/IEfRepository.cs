using Microsoft.EntityFrameworkCore;

namespace Daybreaksoft.Pattern.CQRS.Extensions.EntityFrameworkCore
{
    public interface IEfRepository
    {
        DbContext Db { get; }
    }
}

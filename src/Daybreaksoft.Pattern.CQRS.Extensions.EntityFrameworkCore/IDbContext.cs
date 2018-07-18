using Microsoft.EntityFrameworkCore;

namespace Daybreaksoft.Pattern.CQRS.Extensions.EntityFrameworkCore
{
    public interface IDbContext
    {
        DbContext Db { get; }
    }
}

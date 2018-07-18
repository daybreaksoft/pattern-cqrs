namespace Daybreaksoft.Pattern.CQRS.Extensions.EntityFrameworkCore
{
    /// <summary>
    /// The base type of IQuery
    /// </summary>
    public abstract class AbstractQuery<TDbContext> : IQuery
    {
        protected readonly TDbContext Db;

        protected AbstractQuery(TDbContext db)
        {
            Db = db;
        }
    }
}

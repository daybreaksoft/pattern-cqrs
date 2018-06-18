using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Daybreaksoft.Pattern.CQRS.Extensions.EntityFrameworkCore
{
    public class DefaultUnitOfWork : CQRS.DefaultUnitOfWork
    {
        protected readonly DbContext Db;

        public DefaultUnitOfWork(DbContext db, IAggregateBus aggregateBus, IDynamicRepositoryFactory dynamicRepositoryFactory, IEventBus eventBus) : base(aggregateBus, dynamicRepositoryFactory, eventBus)
        {
            Db = db;
        }

        public override async Task CommitAsync()
        {
            using (var transaction = Db.Database.BeginTransaction())
            {
                try
                {
                    await base.CommitAsync();

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();

                    throw;
                }
            }
        }
    }
}

using System.Data;
using System.Threading.Tasks;

namespace Daybreaksoft.Pattern.CQRS.Extensions.Dapper
{
    public class DefaultUnitOfWork : CQRS.DefaultUnitOfWork
    {
        protected readonly IDbConnection Connection;

        public DefaultUnitOfWork(IDbConnection connection, IAggregateBus aggregateBus, IDynamicRepositoryFactory dynamicRepositoryFactory, IEventBus eventBus) : base(aggregateBus, dynamicRepositoryFactory, eventBus)
        {
            Connection = connection;
        }

        public override async Task CommitAsync()
        {
            //using (var transaction = Connection.Database.BeginTransaction())
            //{
            //    try
            //    {
            //        await base.CommitAsync();

            //        transaction.Commit();
            //    }
            //    catch
            //    {
            //        transaction.Rollback();

            //        throw;
            //    }
            //}
        }
    }
}

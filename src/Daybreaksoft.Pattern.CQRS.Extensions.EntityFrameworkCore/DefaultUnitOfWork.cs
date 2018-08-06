using System;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Daybreaksoft.Pattern.CQRS.DomainModel;
using Daybreaksoft.Pattern.CQRS.Event;
using Microsoft.EntityFrameworkCore.Storage;

namespace Daybreaksoft.Pattern.CQRS.Extensions.EntityFrameworkCore
{
    public class DefaultUnitOfWork : DomainModel.DefaultUnitOfWork
    {
        protected readonly DbContext Db;
        protected IDbContextTransaction Transaction;

        public DefaultUnitOfWork(DbContext db, IEventBus eventBus) : base(eventBus)
        {
            Db = db;
        }

        public override async Task BeginAsync()
        {
            //await base.BeginAsync();

            //Transaction = await Db.Database.BeginTransactionAsync();
        }

        public override async Task CommitAsync()
        {
            foreach (var model in RegisterdModels)
            {
                if (model is IAggregateRootVerification verification)
                {
                    verification.Verify();
                }

                if (model.Action == RegisterAction.Add)
                {
                    await model.PersistService.PersistInsertAsync(model.Model);
                }
            }

            await Db.SaveChangesAsync();

            RegisterdModels.Clear();

            //try
            //{
            //    await base.CommitAsync();

            //    Transaction.Commit();
            //}
            //catch (Exception)
            //{
            //    Transaction.Rollback();

            //    throw;
            //}
        }

        public override void Dispose()
        {
            if (Transaction != null)
            {
                Transaction.Dispose();
                Transaction = null;
            }

            base.Dispose();
        }
    }
}

﻿using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Daybreaksoft.Pattern.CQRS.DomainModel;
using Daybreaksoft.Pattern.CQRS.Event;

namespace Daybreaksoft.Pattern.CQRS.Extensions.EntityFrameworkCore
{
    public class DefaultUnitOfWork : DomainModel.DefaultUnitOfWork
    {
        protected readonly DbContext Db;

        public DefaultUnitOfWork(DbContext db, IRepositoryFactory repositoryFactory, IRepositoryInvoker repositoryInvoker, IEventBus eventBus) : base(repositoryFactory, repositoryInvoker, eventBus)
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

using System;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Daybreaksoft.Pattern.CQRS.Event;
using Microsoft.EntityFrameworkCore.Storage;
using Daybreaksoft.Pattern.CQRS.DomainModel;
using System.Collections.Generic;

namespace Daybreaksoft.Pattern.CQRS.Extensions.EntityFrameworkCore
{
    public class DefaultUnitOfWork : IEfUnitOfWork
    {
        private bool _isCommitted = false;
        private bool _isRollback = false;

        protected readonly DbContext Db;
        protected IDbContextTransaction Transaction;

        protected readonly Dictionary<IEntity, RegisterAddedRepositoryWrapper> AddedEntities;
        protected readonly Dictionary<IEntity, IRepository> ChangedEntities;
        protected readonly Dictionary<IEntity, IRepository> RemovedEntities;

        public DefaultUnitOfWork(DbContext db, IEventBus eventBus)
        {
            Db = db;

            AddedEntities = new Dictionary<IEntity, RegisterAddedRepositoryWrapper>();
            ChangedEntities = new Dictionary<IEntity, IRepository>();
            RemovedEntities = new Dictionary<IEntity, IRepository>();
        }

        public DbContext DbContext => Db;

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (Transaction == null)
            {
                Transaction = await DbContext.Database.BeginTransactionAsync();
            }
            return Transaction;
        }

        public async Task SaveChangesAsync()
        {
            if (AddedEntities.Count > 0 || ChangedEntities.Count > 0 || RemovedEntities.Count > 0)
            {
                foreach (var entity in AddedEntities)
                {
                    await entity.Value.Repository.PersistInsertOf(entity.Key);
                }

                foreach (var entity in ChangedEntities)
                {
                    await entity.Value.PersistUpdateOf(entity.Key);
                }

                foreach (var entity in RemovedEntities)
                {
                    await entity.Value.PersistDeleteOf(entity.Key);
                }

                await this.BeginTransactionAsync();

                try
                {
                    // Save changes.
                    await Db.SaveChangesAsync();
                }
                catch (Exception)
                {
                    Transaction.Rollback();

                    _isRollback = true;

                    throw;
                }

                // Call set key action after added.
                foreach (var entity in AddedEntities)
                {
                    entity.Value.SetKeyAction?.Invoke(entity.Key);
                }

                // Clear tracked entities.
                AddedEntities.Clear();
                ChangedEntities.Clear();
                RemovedEntities.Clear();
            }
        }

        public async Task CommitAsync()
        {
            await SaveChangesAsync();

            if (Transaction != null)
            {
                try
                {
                    Transaction.Commit();

                    _isCommitted = true;
                }
                catch (Exception)
                {
                    Transaction.Rollback();

                    _isRollback = true;

                    throw;
                }
            }
        }

        public async Task RegisterAddedAsync(IEntity entity, IRepository repository, Action<IEntity> setKeyAction)
        {
            AddedEntities.Add(entity, new RegisterAddedRepositoryWrapper(repository, setKeyAction));

            await Task.CompletedTask;
        }

        public async Task RegisterChangedAsync(IEntity entity, IRepository repository)
        {
            ChangedEntities.Add(entity, repository);

            await Task.CompletedTask;
        }

        public async Task RegisterRemovedAsync(IEntity entity, IRepository repository)
        {
            RemovedEntities.Add(entity, repository);

            await Task.CompletedTask;
        }

        public void Dispose()
        {
            if (Transaction != null)
            {
                if (!_isCommitted && !_isRollback)
                {
                    Transaction.Rollback();
                }

                Transaction.Dispose();
            }
        }
    }
}

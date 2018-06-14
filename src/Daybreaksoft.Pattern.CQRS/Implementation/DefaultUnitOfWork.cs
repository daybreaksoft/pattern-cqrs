using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Daybreaksoft.Pattern.CQRS
{
    public class DefaultUnitOfWork : IUnitOfWork
    {
        protected readonly IDependencyInjection DI;

        public DefaultUnitOfWork(IDependencyInjection di)
        {
            DI = di;
        }

        public List<IAggregateRoot> UnCommittedAggregate { get; protected set; } = new List<IAggregateRoot>();

        public virtual async Task OpenAsync()
        {
            //throw new NotImplementedException();
        }

        public virtual async Task CommitAsync()
        {
            await StoreAggreateAsync();
        }

        #region Store Aggreate

        protected virtual async Task StoreAggreateAsync()
        {
            foreach (var aggreate in UnCommittedAggregate)
            {
                if (HasKeyValue(aggreate.Id))
                {
                    if (!aggreate.Deleted)
                    {
                        await UpdateAggreateAsync(aggreate);
                    }
                    else
                    {
                        await RemoveAggreateAsync(aggreate);
                    }
                }
                else
                {
                    await InsertAggreateAsync(aggreate);
                }
            }
        }

        protected virtual bool HasKeyValue(object key)
        {
            if (key is string)
            {
                return !string.IsNullOrEmpty(key.ToString());
            }
            else if (key is int)
            {

                if (int.TryParse(key.ToString(), out int result))
                {
                    return result > 0;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

        protected virtual Type GetRepositoryType(IAggregateRoot aggregate)
        {
            return typeof(IRepository<>).MakeGenericType(aggregate.GetType());
        }

        protected virtual async Task InsertAggreateAsync(IAggregateRoot aggregate)
        {
            var repositoryType = GetRepositoryType(aggregate);

            var repository = DI.GetService(repositoryType);

            var method = repositoryType.GetMethod("InsertAsync");

            await (Task)method.Invoke(repository, new[] { aggregate });
        }

        protected virtual async Task UpdateAggreateAsync(IAggregateRoot aggregate)
        {
            //var repository = GetRepository(aggregate);

            //await repository.UpdateAsync(aggregate);
        }

        protected virtual async Task RemoveAggreateAsync(IAggregateRoot aggregate)
        {
            var repositoryType = GetRepositoryType(aggregate);

            var repository = DI.GetService(repositoryType);

            var method = repositoryType.GetMethod("DeleteAsync");

            await (Task)method.Invoke(repository, new[] { aggregate.Id });
        }

        #endregion

        public virtual TAggregateRoot BuildAggregate<TAggregateRoot>(bool addToUnCommitted = true) where TAggregateRoot : IAggregateRoot, new()
        {
            var aggregate = new TAggregateRoot();

            if (addToUnCommitted) UnCommittedAggregate.Add(aggregate);

            return aggregate;
        }

        public virtual TAggregateRoot GetAggregate<TAggregateRoot>(object id, bool addToUnCommitted = true) where TAggregateRoot : IAggregateRoot, new()
        {
            //var aggregate = DI.GetService<IRepository<TAggregateRoot>>();

            var aggregate = new TAggregateRoot();

            if (addToUnCommitted) UnCommittedAggregate.Add(aggregate);

            return aggregate;
        }
    }
}

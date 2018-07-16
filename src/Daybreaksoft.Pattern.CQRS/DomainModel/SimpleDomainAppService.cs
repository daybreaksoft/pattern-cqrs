using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Daybreaksoft.Extensions.Functions;

namespace Daybreaksoft.Pattern.CQRS.DomainModel
{
    public class SimpleDomainAppService<TAggregateRoot> : IDomainAppService<TAggregateRoot>
        where TAggregateRoot : IAggregateRoot, IEntity
    {
        protected readonly IRepositoryFactory RepositoryFactory;
        protected readonly IRepositoryInvoker RepositoryInvoker;
        protected readonly Type RepositoryType;
        protected readonly object Repository;

        public SimpleDomainAppService(IRepositoryFactory repositoryFactory, IRepositoryInvoker repositoryInvoker)
        {
            RepositoryFactory = repositoryFactory;
            RepositoryInvoker = repositoryInvoker;

            RepositoryType = GetRepositoryType();
            Repository = RepositoryFactory.GetRepository(RepositoryType);
        }

        public virtual async Task<TAggregateRoot> FindAsync(object id)
        {
            var entity = await RepositoryInvoker.FindAsync<TAggregateRoot>(Repository, RepositoryType, id);

            return entity;
        }

        public virtual Task<IEnumerable<TAggregateRoot>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public virtual Task InsertAsync(TAggregateRoot aggregate)
        {
            return RepositoryInvoker.InsertAsync(Repository, RepositoryType, aggregate);
        }

        public virtual async Task UpdateAsync(TAggregateRoot aggregate)
        {
            await RepositoryInvoker.UpdateAsync(Repository, RepositoryType, aggregate);
        }

        public virtual Task DeleteAsync(object id)
        {
            return RepositoryInvoker.RemoveAsync(Repository, RepositoryType, id);
        }

        #region Helper

        protected Type GetRepositoryType()
        {
            var aggregateType = typeof(TAggregateRoot);

            if (aggregateType.GetInterfaces().Any(p=>p == typeof(IEntity)))
            {
                return RepositoryFactory.GetRepositoryType(aggregateType);
            }
            else
            {
                throw new Exception("Allow to call this method when only the instance are both Aggregate and Entity.");
            }
        }

        #endregion
    }
}

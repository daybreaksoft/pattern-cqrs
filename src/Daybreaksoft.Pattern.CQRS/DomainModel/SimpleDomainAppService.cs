using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Daybreaksoft.Extensions.Functions;

namespace Daybreaksoft.Pattern.CQRS.DomainModel
{
    public class SimpleDomainAppService<TAggregateRoot> : IDomainAppService<TAggregateRoot>
        where TAggregateRoot : IAggregateRoot
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

            var entity = await RepositoryInvoker.FindAsync(Repository, RepositoryType, id);

            return (TAggregateRoot) entity;
        }

        public virtual Task<IEnumerable<TAggregateRoot>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public virtual Task InsertAsync(TAggregateRoot aggregate)
        {
            return RepositoryInvoker.InsertAsync(Repository, RepositoryType, (IEntity)aggregate);
        }

        public virtual async Task UpdateAsync(TAggregateRoot aggregate)
        {
            await RepositoryInvoker.UpdateAsync(Repository, RepositoryType, (IEntity)aggregate);
        }

        public virtual Task DeleteAsync(object id)
        {
            return RepositoryInvoker.RemoveAsync(Repository, RepositoryType, id);
        }

        #region Helper

        protected Type GetRepositoryType()
        {
            var aggregateType = typeof(TAggregateRoot);
            var entityType = aggregateType;

            if (aggregateType.GetInterfaces().Any(p=>p == typeof(IEntity)))
            {

#if !NetStandard13
                if (aggregateType.BaseType != null && aggregateType.BaseType != typeof(Object))
                {
                    entityType = aggregateType.BaseType;
                }
#else
                var typeInfo = aggregateType.GetTypeInfo();
                if (typeInfo.BaseType != null && typeInfo.BaseType != typeof(Object))
                {
                    entityType = typeInfo.BaseType;
                }
#endif

                return RepositoryFactory.GetRepositoryType(entityType);
            }
            else
            {
                throw new Exception("Allow to call this method when only the instance are both Aggregate and Entity.");
            }
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daybreaksoft.Pattern.CQRS.DomainModel
{
    public class DefaultDomainAppService<TAggregateRoot> : IDomainAppService<TAggregateRoot>
        where TAggregateRoot : IAggregateRoot
    {
        protected readonly IRepositoryFactory RepositoryFactory;
        protected readonly IRepositoryInvoker RepositoryInvoker;

        public DefaultDomainAppService(IRepositoryFactory repositoryFactory, IRepositoryInvoker repositoryInvoker)
        {
            RepositoryFactory = repositoryFactory;
            RepositoryInvoker = repositoryInvoker;
        }

        public virtual Task<TAggregateRoot> FindAsync(object id)
        {
            throw new NotImplementedException();
        }

        public virtual Task<IEnumerable<TAggregateRoot>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public virtual async Task InsertAsync(TAggregateRoot aggregate)
        {
            if (aggregate is IEntity)
            {
                var type = aggregate.GetType();

                if (type.BaseType != null && type.BaseType != typeof(Object))
                {
                    type = type.BaseType;
                }

                var repositoryType = RepositoryFactory.GetRepositoryType(type);

                var repository = RepositoryFactory.GetRepository(repositoryType);

                await RepositoryInvoker.InsertAsync(repository, repositoryType, (IEntity)aggregate);
            }
            else
            {
                throw new Exception("Allow to call this method when only the instance are both Aggregate and Entity.");
            }
        }

        public virtual Task UpdateAsync(TAggregateRoot aggregate)
        {
            throw new NotImplementedException();
        }

        public virtual Task DeleteAsync(object id)
        {
            throw new NotImplementedException();
        }

        public virtual Task DeleteAsync(TAggregateRoot aggregate)
        {
            throw new NotImplementedException();
        }
    }
}

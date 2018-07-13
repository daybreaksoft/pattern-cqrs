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

        public DefaultDomainAppService(IRepositoryFactory repositoryFactory)
        {
            RepositoryFactory = repositoryFactory;
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
                //await RepositoryFactory.InvokeInsertAsync((IEntity)aggregate);

                var type = aggregate.GetType();


#if !NetStandar13

                var interfaces = type.GetInterfaces();

                await RepositoryFactory.InvokeInsertAsync((IEntity)type.BaseType);
#endif
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

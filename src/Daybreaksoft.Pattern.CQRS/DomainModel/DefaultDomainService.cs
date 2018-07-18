using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Daybreaksoft.Pattern.CQRS.DomainModel
{
    public class DefaultDomainService<TAggregateRoot> : IDomainService<TAggregateRoot>
        where TAggregateRoot : IAggregateRoot, IEntity
    {
        protected readonly IRepositoryFactory RepositoryFactory;

        public DefaultDomainService(IRepositoryFactory repositoryFactory)
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

        public virtual Task InsertAsync(TAggregateRoot aggregate)
        {
            throw new NotImplementedException();
        }

        public virtual async Task UpdateAsync(TAggregateRoot aggregate)
        {
            throw new NotImplementedException();
        }

        public virtual Task DeleteAsync(object id)
        {
            throw new NotImplementedException();
        }

        #region Repository Helper

     

        #endregion
    }
}

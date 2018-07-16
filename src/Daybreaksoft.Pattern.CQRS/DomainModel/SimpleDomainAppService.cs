using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Daybreaksoft.Pattern.CQRS.DomainModel
{
    public class SimpleDomainAppService<TAggregateRoot> : IDomainAppService<TAggregateRoot>
        where TAggregateRoot : IAggregateRoot, IEntity
    {
        protected readonly IRepository<TAggregateRoot> Repository;

        public SimpleDomainAppService(IRepository<TAggregateRoot> repository)
        {
            Repository = repository;
        }

        public virtual Task<TAggregateRoot> FindAsync(object id)
        {
            return Repository.FindAsync(id);
        }

        public virtual Task<IEnumerable<TAggregateRoot>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public virtual Task InsertAsync(TAggregateRoot aggregate)
        {
            return Repository.InsertAsync(aggregate);
        }

        public virtual async Task UpdateAsync(TAggregateRoot aggregate)
        {
            await Repository.UpdateAsync(aggregate);
        }

        public virtual Task DeleteAsync(object id)
        {
            return Repository.DeleteAsync(id);
        }
    }
}

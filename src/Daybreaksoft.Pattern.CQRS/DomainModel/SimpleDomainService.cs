﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Daybreaksoft.Pattern.CQRS.DomainModel
{
    public class SimpleDomainService<TAggregateRoot> : IDomainService<TAggregateRoot>
        where TAggregateRoot : IAggregateRoot, IEntity
    {
        protected readonly IRepository<TAggregateRoot> Repository;

        public SimpleDomainService(IRepository<TAggregateRoot> repository)
        {
            Repository = repository;
        }

        public virtual Task<TAggregateRoot> FindAsync(object id)
        {
            return Repository.FindAsync(id);
        }

        public virtual Task<List<TAggregateRoot>> FindAllAsync()
        {
            return Repository.FindAllAsync();
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

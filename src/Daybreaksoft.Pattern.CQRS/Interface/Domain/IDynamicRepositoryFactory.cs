using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daybreaksoft.Pattern.CQRS
{
    public interface IDynamicRepositoryFactory
    {
        Type GetRepositoryType(IAggregateRoot aggregate);

        Type GetRepositoryType(Type aggregateType);

        object GetRepository(IAggregateRoot aggregate);

        object GetRepository(Type aggregateType);

        Task InvokeInsertAsync(IAggregateRoot aggregate);

        Task InvokeUpdateAsync(IAggregateRoot aggregate);

        Task InvokeRemoveAsync<TAggregateRoot>(object id) where TAggregateRoot : IAggregateRoot;

        Task InvokeRemoveAsync(Type aggregateType, object id);

        Task<TAggregateRoot> InvokeFindAsync<TAggregateRoot>(object id) where TAggregateRoot : IAggregateRoot;
    }
}

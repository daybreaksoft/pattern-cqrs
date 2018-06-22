using System;
using System.Collections.Generic;
using System.Data;
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

        Task InvokeInsertAsync(IAggregateRoot aggregate, IDbTransaction transaction = null);

        Task InvokeUpdateAsync(IAggregateRoot aggregate, IDbTransaction transaction = null);

        Task InvokeRemoveAsync<TAggregateRoot>(object id, IDbTransaction transaction = null) where TAggregateRoot : IAggregateRoot;

        Task InvokeRemoveAsync(Type aggregateType, object id, IDbTransaction transaction = null);

        Task<TAggregateRoot> InvokeFindAsync<TAggregateRoot>(object id, IDbTransaction transaction = null) where TAggregateRoot : IAggregateRoot;
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Daybreaksoft.Pattern.CQRS.DomainModel
{
    public interface IDomainServiceFactory
    {
        IDomainService<TAggregateRoot> GetDomainService<TAggregateRoot>() where TAggregateRoot : IAggregateRoot;
    }
}

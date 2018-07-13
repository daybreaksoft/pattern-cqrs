using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Daybreaksoft.Pattern.CQRS.DomainModel
{
    public interface IDomainAppServiceFactory
    {
        IDomainAppService<TAggregateRoot> GetDomainAppService<TAggregateRoot>() where TAggregateRoot : IAggregateRoot;
    }
}

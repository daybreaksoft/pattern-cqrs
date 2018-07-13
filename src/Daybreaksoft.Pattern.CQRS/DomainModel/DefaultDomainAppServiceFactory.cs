using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daybreaksoft.Pattern.CQRS.DomainModel
{
    public class DefaultDomainAppServiceFactory : IDomainAppServiceFactory
    {
        protected readonly IDependencyInjection DI;

        public DefaultDomainAppServiceFactory(IDependencyInjection di)
        {
            DI = di;
        }

        public IDomainAppService<TAggregateRoot> GetDomainAppService<TAggregateRoot>() where TAggregateRoot : IAggregateRoot
        {
            return DI.GetService<IDomainAppService<TAggregateRoot>>();
        }
    }
}

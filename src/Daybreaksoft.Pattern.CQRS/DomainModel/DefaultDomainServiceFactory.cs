using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daybreaksoft.Pattern.CQRS.DomainModel
{
    public class DefaultDomainServiceFactory : IDomainServiceFactory
    {
        protected readonly IDependencyInjection DI;

        public DefaultDomainServiceFactory(IDependencyInjection di)
        {
            DI = di;
        }

        public IDomainService<TAggregateRoot> GetDomainService<TAggregateRoot>() where TAggregateRoot : IAggregateRoot
        {
            return DI.GetService<IDomainService<TAggregateRoot>>();
        }
    }
}

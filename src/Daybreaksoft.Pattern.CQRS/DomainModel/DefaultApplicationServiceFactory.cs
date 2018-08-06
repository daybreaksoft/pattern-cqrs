namespace Daybreaksoft.Pattern.CQRS.DomainModel
{
    public class DefaultApplicationServiceFactory : IApplicationServiceFactory
    {
        protected readonly IDependencyInjection DI;

        public DefaultApplicationServiceFactory(IDependencyInjection di)
        {
            DI = di;
        }

        public IApplicationService<TAggregateRoot> GetApplicationService<TAggregateRoot>() where TAggregateRoot : IAggregateRoot
        {
            return DI.GetService<IApplicationService<TAggregateRoot>>();
        }
    }
}

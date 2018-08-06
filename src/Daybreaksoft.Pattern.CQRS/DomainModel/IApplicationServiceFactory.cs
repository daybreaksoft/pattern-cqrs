namespace Daybreaksoft.Pattern.CQRS.DomainModel
{
    public interface IApplicationServiceFactory
    {
        IApplicationService<TAggregateRoot> GetApplicationService<TAggregateRoot>() where TAggregateRoot : IAggregateRoot;
    }
}

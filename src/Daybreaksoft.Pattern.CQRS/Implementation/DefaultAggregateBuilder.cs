using System.Threading.Tasks;

namespace Daybreaksoft.Pattern.CQRS
{
    /// <summary>
    /// Defualt implemention of IAggregateBuilder
    /// </summary>
    public class DefaultAggregateBuilder : IAggregateBuilder
    {
        protected readonly IDynamicRepositoryFactory DynamicRepositoryFactory;

        public DefaultAggregateBuilder(IDynamicRepositoryFactory dynamicRepositoryFactory)
        {
            DynamicRepositoryFactory = dynamicRepositoryFactory;
        }

        public TAggregateRoot BuildAggregate<TAggregateRoot>() where TAggregateRoot : IAggregateRoot, new()
        {
            return new TAggregateRoot();
        }

        public async Task<TAggregateRoot> GetAggregate<TAggregateRoot>(object id) where TAggregateRoot : IAggregateRoot
        {
            return await DynamicRepositoryFactory.InvokeFindAsync<TAggregateRoot>(id);
        }
    }
}

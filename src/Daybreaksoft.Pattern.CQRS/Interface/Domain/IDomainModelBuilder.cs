namespace Daybreaksoft.Pattern.CQRS
{
    public interface IDomainModelBuilder
    {
        TModel BuildModel<TModel>() where TModel : IAggregateRoot;

        TModel BuildModel<TModel>(object id) where TModel : IAggregateRoot;
    }
}

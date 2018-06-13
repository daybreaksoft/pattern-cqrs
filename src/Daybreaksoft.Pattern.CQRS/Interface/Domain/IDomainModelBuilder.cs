namespace Daybreaksoft.Pattern.CQRS
{
    public interface IDomainModelBuilder
    {
        TModel BuildModel<TModel>() where TModel : IDomainModel;

        TModel BuildModel<TModel>(object id) where TModel : IDomainModel;
    }
}

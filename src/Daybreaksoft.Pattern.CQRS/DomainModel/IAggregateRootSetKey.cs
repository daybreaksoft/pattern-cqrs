namespace Daybreaksoft.Pattern.CQRS.DomainModel
{
    /// <summary>
    /// The interface of IAggregateRootSetKey
    /// </summary>
    public interface IAggregateRootSetKey
    {
        void SetKey(object id);
    }
}

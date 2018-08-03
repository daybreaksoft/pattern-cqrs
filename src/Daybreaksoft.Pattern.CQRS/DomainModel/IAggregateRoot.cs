namespace Daybreaksoft.Pattern.CQRS.DomainModel
{
    /// <summary>
    /// The interface of Aggregate Root
    /// </summary>
    public interface IAggregateRoot
    {
        /// <summary>
        /// The key of Aggregate Root
        /// </summary>
        object Id { get; }
    }
}

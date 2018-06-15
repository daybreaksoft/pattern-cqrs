namespace Daybreaksoft.Pattern.CQRS
{
    /// <summary>
    /// Domain model
    /// </summary>
    public interface IAggregateRoot
    {
        object Id { get; }

        AggregateState State { get; }
    }
}

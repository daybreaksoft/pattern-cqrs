namespace Daybreaksoft.Pattern.CQRS
{
    /// <summary>
    /// Domain model
    /// </summary>
    public interface IDomainModel
    {
        /// <summary>
        /// Key of model
        /// </summary>
        object Id { get; set; }
    }
}

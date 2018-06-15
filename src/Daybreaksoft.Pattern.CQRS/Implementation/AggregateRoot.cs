using System.ComponentModel.DataAnnotations.Schema;

namespace Daybreaksoft.Pattern.CQRS
{
    /// <summary>
    /// Abstract AggregateRoot
    /// </summary>
    public abstract class AggregateRoot : IAggregateRoot
    {
        public abstract object Id { get; }

        [NotMapped]
        public virtual bool Deleted { get; protected set; }

        public virtual void Remove()
        {
            Deleted = true;
        }
    }
}

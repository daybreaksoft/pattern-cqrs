using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using Daybreaksoft.Extensions.Functions;

namespace Daybreaksoft.Pattern.CQRS
{
    /// <summary>
    /// Abstract AggregateRoot
    /// </summary>
    public abstract class AggregateRoot : IAggregateRoot
    {
        public abstract object Id { get; }

        [NotMapped]
        public virtual bool Deleted { get; protected set; } = false;

        public virtual void Remove()
        {
            Deleted = true;
        }
    }
}

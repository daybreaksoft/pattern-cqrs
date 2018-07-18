using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daybreaksoft.Pattern.CQRS.DomainModel
{
    public interface IDataMap<TAggregate, TEntity>
        where TAggregate : IAggregateRoot
        where TEntity : IEntity
    {
        TAggregate ConvertToAggregate(TEntity entity);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace Daybreaksoft.Pattern.CQRS
{
    public class AggregateCollection : IEnumerable<IAggregateRoot>
    {
        protected readonly List<IAggregateRoot> Aggregates;

        public AggregateCollection()
        {
            Aggregates = new List<IAggregateRoot>();
        }

        public void Add(IAggregateRoot aggregate)
        {
            if (aggregate == null) throw new ArgumentNullException(nameof(aggregate));

            if (!Aggregates.Any(p => p == aggregate))
            {
                Aggregates.Add(aggregate);
            }
        }

        public IEnumerator<IAggregateRoot> GetEnumerator()
        {
            return Aggregates.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

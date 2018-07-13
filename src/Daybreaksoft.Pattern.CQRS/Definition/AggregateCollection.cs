using System;
using System.Collections.Generic;
using System.Linq;
using Daybreaksoft.Pattern.CQRS.DomainModel;

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

            if (Aggregates.Any(p => p == aggregate))
            {
                throw new Exception("Already added one aggregate, cannot add another same one.");
            }

            if (Aggregates.Any(p => p.Id != null && p.Id == aggregate.Id))
            {
                throw new Exception($"Already added on aggregate that has key {aggregate.Id}, cannot add another one.");
            }

            Aggregates.Add(aggregate);
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

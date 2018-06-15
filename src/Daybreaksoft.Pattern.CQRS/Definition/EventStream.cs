using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daybreaksoft.Pattern.CQRS
{
    public class EventStream : IEnumerable<IEvent>
    {
        protected readonly List<IEvent> Events;

        public EventStream()
        {
            Events = new List<IEvent>();
        }

        public void Append(IEvent evet)
        {
            if (evet == null) throw new ArgumentNullException(nameof(evet));

            Events.Add(evet);
        }

        public IEnumerator<IEvent> GetEnumerator()
        {
            return Events.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

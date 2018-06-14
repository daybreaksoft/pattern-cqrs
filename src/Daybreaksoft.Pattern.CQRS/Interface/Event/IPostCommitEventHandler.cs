using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daybreaksoft.Pattern.CQRS
{
    public interface IPostCommitEventHandler<in TEvent>
        where TEvent : IEvent
    {
        Task HandleAsync(TEvent evnt);
    }
}

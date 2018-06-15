using Daybreaksoft.Pattern.CQRS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.Sample.Domain.Models
{
    public class TrafficViolationAddedEventHandler : IEventHandler<TrafficViolationAddedEvent>
    {
        public Task HandleAsync(TrafficViolationAddedEvent evnt)
        {
            throw new NotImplementedException();
        }
    }
}

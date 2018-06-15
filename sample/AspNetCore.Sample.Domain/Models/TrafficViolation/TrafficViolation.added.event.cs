using Daybreaksoft.Pattern.CQRS;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore.Sample.Domain.Models
{
    public class TrafficViolationAddedEvent : IEvent
    {
        public int VehicleId { get; set; }

        public int DeductPoint { get; set; }
    }
}

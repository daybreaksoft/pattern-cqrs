using Daybreaksoft.Pattern.CQRS;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCore.Sample.Domain.Models
{
    public partial class TrafficViolation : DefaultAggregateRoot
    {
        [NotMapped]
        public override object Id => TrafficViolationId;
    }
}

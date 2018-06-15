using System.ComponentModel.DataAnnotations.Schema;
using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Sample.Domain.Models
{
    public partial class Vehicle : DefaultAggregateRoot
    {
        [NotMapped]
        public override object Id => VehicleId;
    }
}

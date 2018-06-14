using AspNetCore.Sample.Repository.Entities;
using Daybreaksoft.Pattern.CQRS;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCore.Sample.Domain.Models
{
    public partial class TrafficViolation : AggregateRoot
    {
        [NotMapped]
        public override object Id { get { return TrafficViolationId; } }
    }
}

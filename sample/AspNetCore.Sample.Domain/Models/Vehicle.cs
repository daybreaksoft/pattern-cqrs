using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using AspNetCore.Sample.Repository.Entities;
using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Sample.Domain.Models
{
    public partial class Vehicle : AggregateRoot
    {
        [NotMapped]
        public override object Id { get { return VehicleId; } }
    }
}

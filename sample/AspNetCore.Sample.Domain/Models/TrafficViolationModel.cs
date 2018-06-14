using AspNetCore.Sample.Repository.Entities;
using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Sample.Domain.Models
{
    public class TrafficViolationModel : DefaultDomainModel<TrafficViolation>
    {
        public TrafficViolationModel(IRepository<TrafficViolation> repository) : base(repository)
        {
        }

        public int VehicleId { get; set; }

        public int DeductPoint { get; set; }
    }
}

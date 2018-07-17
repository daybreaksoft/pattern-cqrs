using Daybreaksoft.Pattern.CQRS.DomainModel;

namespace AspNetCore.EF.Sample.Data.Entities
{
    public class TrafficViolationEntity : IEntity
    {
        public int Id { get; set; }

        public int VehicleId { get; set; }

        public int DeductPoint { get; set; }
    }
}

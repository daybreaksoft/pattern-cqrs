using System.ComponentModel.DataAnnotations;
using Daybreaksoft.Pattern.CQRS.DomainModel;

namespace AspNetCore.EF.Sample.Data.Entities
{
    public class TrafficViolationEntity : IEntity
    {
        [Key]
        public int Id { get; set; }

        public int VehicleId { get; set; }

        public int DeductPoint { get; set; }
    }
}

using Daybreaksoft.Pattern.CQRS.DomainModel;

namespace AspNetCore.EF.Sample.Data.Entities
{
    public class VehicleEntity : IEntity
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string PlateNumber { get; set; }
    }
}

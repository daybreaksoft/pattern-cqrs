using System.Threading.Tasks;
using AspNetCore.Sample.Repository.Entities;
using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Sample.Domain.Models
{
    public class VehicleModel : DefaultDomainModel<Vehicle>
    {
        public VehicleModel(IRepository<Vehicle> repository) : base(repository)
        {
        }

        public int UserId { get; set; }

        public string PlateNumber { get; set; }
    }
}

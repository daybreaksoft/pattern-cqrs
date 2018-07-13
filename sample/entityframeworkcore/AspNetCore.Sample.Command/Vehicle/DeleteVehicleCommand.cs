using Daybreaksoft.Pattern.CQRS;
using Daybreaksoft.Pattern.CQRS.Command;

namespace AspNetCore.Sample.Command
{
    public class DeleteVehicleCommand : ICommand
    {
        public int VehicleId { get; set; }
    }
}

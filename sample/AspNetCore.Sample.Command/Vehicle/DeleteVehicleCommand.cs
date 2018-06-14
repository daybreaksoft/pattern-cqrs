using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Sample.Command.Vehicle
{
    public class DeleteVehicleCommand : ICommand
    {
        public int VehicleId { get; set; }
    }
}

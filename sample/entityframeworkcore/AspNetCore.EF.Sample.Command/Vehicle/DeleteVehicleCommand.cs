using Daybreaksoft.Pattern.CQRS.Command;

namespace AspNetCore.EF.Sample.Command.Vehicle
{
    public class DeleteVehicleCommand : ICommand
    {
        public int VehicleId { get; set; }
    }
}

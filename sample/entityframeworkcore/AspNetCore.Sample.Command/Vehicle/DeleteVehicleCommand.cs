using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Sample.Command
{
    public class DeleteVehicleCommand : ICommand
    {
        public int VehicleId { get; set; }
    }
}

using Daybreaksoft.Pattern.CQRS.Command;

namespace AspNetCore.EF.Sample.Command.Vehicle
{
    public class UpdateVehicleCommand : ICommand
    {
        public int VehicleId { get; set; }

        public int UserId { get; set; }

        public string PlateNumber { get; set; }
    }
}

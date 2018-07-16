using Daybreaksoft.Pattern.CQRS.Command;

namespace AspNetCore.EF.Sample.Command.Vehicle
{
    public class CreateVehicleCommand : ICommand
    {
        public int UserId { get; set; }

        public string PlateNumber { get; set; }
    }
}

using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Sample.Command
{
    public class CreateVehicleCommand : ICommand
    {
        public int UserId { get; set; }

        public string PlateNumber { get; set; }
    }
}

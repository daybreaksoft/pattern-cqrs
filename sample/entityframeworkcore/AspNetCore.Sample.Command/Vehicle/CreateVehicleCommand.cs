using Daybreaksoft.Pattern.CQRS;
using Daybreaksoft.Pattern.CQRS.Command;

namespace AspNetCore.Sample.Command
{
    public class CreateVehicleCommand : ICommand
    {
        public int UserId { get; set; }

        public string PlateNumber { get; set; }
    }
}

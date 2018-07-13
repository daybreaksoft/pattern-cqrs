using Daybreaksoft.Pattern.CQRS;
using Daybreaksoft.Pattern.CQRS.Command;

namespace AspNetCore.Sample.Command
{
    public class CreateTrafficViolationCommand : ICommand
    {
        public int VehicleId { get; set; }

        public int DeductPoint { get; set; }
    }
}

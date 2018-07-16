using Daybreaksoft.Pattern.CQRS.Command;

namespace AspNetCore.EF.Sample.Command.TrafficViolation
{
    public class CreateTrafficViolationCommand : ICommand
    {
        public int VehicleId { get; set; }

        public int DeductPoint { get; set; }
    }
}

using Daybreaksoft.Pattern.CQRS.Command;

namespace AspNetCore.EF.Sample.Command.TrafficViolation
{
    public class DeleteTrafficViolationCommand : ICommand
    {
        public int TrafficViolationId { get; set; }
    }
}

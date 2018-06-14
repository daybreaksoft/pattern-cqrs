using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Sample.Command.TrafficViolation
{
    public class DeleteTrafficViolationCommand : ICommand
    {
        public int TrafficViolationId { get; set; }
    }
}

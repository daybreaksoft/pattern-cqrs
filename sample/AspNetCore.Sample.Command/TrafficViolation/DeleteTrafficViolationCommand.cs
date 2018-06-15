using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Sample.Command
{
    public class DeleteTrafficViolationCommand : ICommand
    {
        public int TrafficViolationId { get; set; }
    }
}

using Daybreaksoft.Pattern.CQRS;
using Daybreaksoft.Pattern.CQRS.Command;

namespace AspNetCore.Sample.Command
{
    public class DeleteTrafficViolationCommand : ICommand
    {
        public int TrafficViolationId { get; set; }
    }
}

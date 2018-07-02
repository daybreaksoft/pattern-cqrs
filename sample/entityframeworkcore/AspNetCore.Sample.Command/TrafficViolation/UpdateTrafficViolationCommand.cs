namespace AspNetCore.Sample.Command
{
    public class UpdateTrafficViolationCommand : CreateTrafficViolationCommand
    {
        public int TrafficViolationId { get; set; }
    }
}

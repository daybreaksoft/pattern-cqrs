namespace AspNetCore.EF.Sample.Command.TrafficViolation
{
    public class UpdateTrafficViolationCommand : CreateTrafficViolationCommand
    {
        public int TrafficViolationId { get; set; }
    }
}

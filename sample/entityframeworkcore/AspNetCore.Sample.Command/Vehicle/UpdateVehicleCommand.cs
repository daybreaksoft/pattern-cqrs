namespace AspNetCore.Sample.Command
{
    public class UpdateVehicleCommand : CreateVehicleCommand
    {
        public int VehicleId { get; set; }
    }
}

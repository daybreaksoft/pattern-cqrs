namespace AspNetCore.EF.Sample.Command.Vehicle
{
    public class UpdateVehicleCommand : CreateVehicleCommand
    {
        public int VehicleId { get; set; }
    }
}

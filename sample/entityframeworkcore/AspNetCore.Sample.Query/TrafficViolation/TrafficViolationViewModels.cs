namespace AspNetCore.Sample.Query.TrafficViolation
{
    public class TrafficViolationViewModel
    {
        public int VehicleId { get; set; }

        public int DeductPoint { get; set; }
    }

    public class TrafficViolationListItemViewModel
    {
        public int Id { get; set; }

        public string VehiclePlateNumber { get; set; }

        public int DeductPoint { get; set; }
    }
}

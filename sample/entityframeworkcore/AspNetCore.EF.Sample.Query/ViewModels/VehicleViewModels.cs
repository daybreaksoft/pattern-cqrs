namespace AspNetCore.EF.Sample.Query.ViewModels
{
    public class VehicleViewModel
    {
        public int UserId { get; set; }
        
        public string Username { get; set; }

        public string PlateNumber { get; set; }
    }


    public class VehicleSelectListItemViewModel
    {
        public int Id { get; set; }

        public string PlateNumber { get; set; }
    }

    public class VehicleListItemViewModel
    {
        public int Id { get; set; }

        public string PlateNumber { get; set; }

        public string Username { get; set; }

        public int UserPoint { get; set; }
    }
}

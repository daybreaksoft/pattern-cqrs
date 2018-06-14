using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore.Sample.Query.Vehicle
{
    public class VehicleViewModel
    {
        public int VehicleId { get; set; }

        public string PlateNumber { get; set; }

        public int UserId { get; set; }
    }

    public class VehicleSelectListItemViewModel
    {
        public int VehicleId { get; set; }

        public string PlateNumber { get; set; }
    }

    public class VehicleListItemViewModel
    {
        public int VehicleId { get; set; }

        public string PlateNumber { get; set; }

        public string Username { get; set; }

        public int UserPoint { get; set; }
    }
}

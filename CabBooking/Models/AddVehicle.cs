using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CabBooking.Models
{
    public class AddVehicle
    {
        public string VehicleNumber { get; set; }
        public string VehicleName { get; set; }
        public string VehicleDescription { get; set; }
        public string Fule { get; set; }
        public string Purchase { get; set; }
        public string DriverName { get; set; }
        public string VehicleType { get; set; }
        public string VehicleModel { get; set; }
        public string SeatingCapacity { get; set; }
        public string PricePerKm { get; set; }
        public string PricePerM { get; set; }
        public string MinimumFare { get; set; }
        public string Commission { get; set; }
        public string PassengerCancelTimeLimit { get; set; }
        public string PassengerCancelCharge { get; set; }
        public string VehiclePhoto { get; set; }
        public int Id { get; set; }
        public int IsDeleted { get; set; }
        public int ProcId { get; set; }
        public List<AddVehicle> list { get; set; }
    }

    public class VehicleImage
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public string VehiclePhoto { get; set; }
    }

}
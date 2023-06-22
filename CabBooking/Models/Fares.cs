using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CabBooking.Models
{
    public class Fares
    {
        public int FareID { get; set; }
        public string VehicleType { get; set; }
        public decimal FareKM { get; set; }
        public decimal MinFare { get; set; }
        public decimal MinDistance { get; set; }
        public decimal WaitFare { get; set; }
        public int IsDeleted { get; set; }

        public List<Fares> list { get; set; }
    }
}
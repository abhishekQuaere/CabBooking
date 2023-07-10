using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CabBooking.Models
{
    public class MainModel
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

        public string Source { get; set; }
        public string Destination { get; set; }
        public string TotalPrices { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public List<MainModel> list { get; set; }
    }

    public class Copyright
    {
        public string text { get; set; }
        public string imageUrl { get; set; }
        public string imageAltText { get; set; }
    }

    public class DisplayLatLng
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Info
    {
        public int statuscode { get; set; }
        public Copyright copyright { get; set; }
        public List<object> messages { get; set; }
    }

    public class LatLng
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Location
    {
        public string Street { get; set; }
        public string adminArea6 { get; set; }
        public string adminArea6Type { get; set; }
        public string adminArea5 { get; set; }
        public string adminArea5Type { get; set; }
        public string adminArea4 { get; set; }
        public string adminArea4Type { get; set; }
        public string adminArea3 { get; set; }
        public string adminArea3Type { get; set; }
        public string adminArea1 { get; set; }
        public string adminArea1Type { get; set; }
        public string postalCode { get; set; }
        public string geocodeQualityCode { get; set; }
        public string geocodeQuality { get; set; }
        public bool dragPoint { get; set; }
        public string sideOfStreet { get; set; }
        public string linkId { get; set; }
        public string unknownInput { get; set; }
        public string type { get; set; }
        public LatLng latLng { get; set; }
        public DisplayLatLng displayLatLng { get; set; }
        public string mapUrl { get; set; }
    }

    public class Options
    {
        public int maxResults { get; set; }
        public bool ignoreLatLngInput { get; set; }
    }

    public class ProvidedLocation
    {
        public string location { get; set; }
    }

    public class Result
    {
        public ProvidedLocation providedLocation { get; set; }
        public List<Location> locations { get; set; }
    }

    public class Root
    {
        public Info info { get; set; }
        public Options options { get; set; }
        public List<Result> results { get; set; }
    }

    public class DistanceCalculator
    {
        private const double EarthRadiusKm = 6371.0;

        public double CalculateDistance(LatLng source, LatLng destination)
        {
            double sourceLatRadians = ConvertToRadians(source.lat);
            double destLatRadians = ConvertToRadians(destination.lat);

            double latDiffRadians = ConvertToRadians(destination.lat - source.lat);
            double lonDiffRadians = ConvertToRadians(destination.lng - source.lng);

            double a = Math.Sin(latDiffRadians / 2) * Math.Sin(latDiffRadians / 2) +
                       Math.Cos(sourceLatRadians) * Math.Cos(destLatRadians) *
                       Math.Sin(lonDiffRadians / 2) * Math.Sin(lonDiffRadians / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            double distance = EarthRadiusKm * c;
            return distance;
        }

        private double ConvertToRadians(double degrees)
        {
            return degrees * (Math.PI / 180);
        }


        
    }
}
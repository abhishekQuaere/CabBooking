using Dapper;
using CabBooking.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using CabBooking.DBContext;

namespace CabBooking.DbRepository
{

    public class AdminDb
    {
        DapperDbContext _dapper = new DapperDbContext();

        public T AddUpdateVehicle<T>(AddVehicle obj)
        {
            try
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("Id", obj.Id);
                dynamicParameters.Add("VehicleNumber", obj.VehicleNumber);
                dynamicParameters.Add("VehicleName", obj.VehicleName);
                dynamicParameters.Add("VehicleDescription", obj.VehicleDescription);
                dynamicParameters.Add("VehicleType", obj.VehicleType);
                dynamicParameters.Add("Purchase", obj.Purchase);
                dynamicParameters.Add("VehicleModel", obj.VehicleModel);
                dynamicParameters.Add("SeatingCapacity", obj.SeatingCapacity);
                dynamicParameters.Add("PricePerKm", obj.PricePerKm);
                dynamicParameters.Add("PricePerM", obj.PricePerM);
                dynamicParameters.Add("MinimumFare", obj.MinimumFare);
                dynamicParameters.Add("Commission", obj.Commission);
                dynamicParameters.Add("PassengerCancelTimeLimit", obj.PassengerCancelTimeLimit);
                dynamicParameters.Add("PassengerCancelCharge", obj.PassengerCancelCharge);
                dynamicParameters.Add("Fule", obj.Fule);
                dynamicParameters.Add("DriverName", obj.DriverName);
                dynamicParameters.Add("IsDeleted", obj.IsDeleted);
                return _dapper.ExecuteGet<T>("Proc_AddUpdateVehicle", dynamicParameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<T> GetAllBooking<T>(MainModel obj)
        {
            try
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("ProcId", obj.ProcId);
                dynamicParameters.Add("Id", obj.Id);
                dynamicParameters.Add("Source", obj.Source);
                dynamicParameters.Add("Destination", obj.Destination);
                dynamicParameters.Add("TotalPrices", obj.TotalPrices);
                dynamicParameters.Add("Name", obj.Name);
                dynamicParameters.Add("MobileNo", obj.MobileNo);
                return _dapper.GetAll<T>("Proc_AddBooking", dynamicParameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<T> GetAllVehicle<T>(AddVehicle obj)
        {
            try
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("Id", obj.Id);
                dynamicParameters.Add("ProcId", obj.ProcId);
                return _dapper.GetAll<T>("Proc_GetAllVehicle", dynamicParameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public T GetVehicle<T>(AddVehicle obj)
        {
            try
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("Id", obj.Id);
                dynamicParameters.Add("ProcId", obj.ProcId);
                return _dapper.Get<T>("Proc_GetAllVehicle", dynamicParameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public T AddVehicleImageById<T>(VehicleImage obj)
        {
            try
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("Id", obj.Id);
                dynamicParameters.Add("VehicleId", obj.VehicleId);
                dynamicParameters.Add("VehiclePhoto", obj.VehiclePhoto);
                return _dapper.ExecuteGet<T>("Proc_AddVehiclePhoto", dynamicParameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<T> GetVehicleImageById<T>(VehicleImage obj)
        {
            try
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("VehicleId", obj.VehicleId);
                return _dapper.GetAll<T>("Proc_GetVehicleImageById", dynamicParameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public T AddUpdateFare<T>(Fares obj)
        {
            try
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
               
                dynamicParameters.Add("FareID", obj.FareID);
                dynamicParameters.Add("VehicleType", obj.VehicleType);
                dynamicParameters.Add("FareKM", obj.FareKM);
                dynamicParameters.Add("MinFare", obj.MinFare);
                dynamicParameters.Add("WaitFare", obj.WaitFare);
                dynamicParameters.Add("MinDistance", obj.MinDistance);
            
                dynamicParameters.Add("IsDeleted", obj.IsDeleted);
                return _dapper.ExecuteGet<T>("Proc_AddUpdateFares", dynamicParameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<T> GetAllFare<T>(Fares obj)
        {
            try
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("FareID", obj.FareID);
      
                return _dapper.GetAll<T>("GetAllFares", dynamicParameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
using CabBooking.DBContext;
using CabBooking.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CabBooking.DbRepository
{
    public class HomeDb
    {
        DapperDbContext _dapper = new DapperDbContext();

        public List<T> GetAllVehicle<T>(MainModel obj)
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

        public List<SelectListItem> GetVehicles()
        {
            try
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                return _dapper.GetAll<SelectListItem>("Proc_VehicleDropDown", dynamicParameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public T AddBooking<T>(MainModel obj)
        {
            try
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("ProcId", 1);
                dynamicParameters.Add("Id", obj.Id);
                dynamicParameters.Add("Source", obj.Source);
                dynamicParameters.Add("Destination", obj.Destination);
                dynamicParameters.Add("TotalPrices", obj.TotalPrices);
                dynamicParameters.Add("Name", obj.Name);
                dynamicParameters.Add("MobileNo", obj.MobileNo);
                dynamicParameters.Add("PickupDate", obj.PickupDate);
                dynamicParameters.Add("PickupTime", obj.PickupTime);
                dynamicParameters.Add("ReturnDate", obj.ReturnDate);
                dynamicParameters.Add("ReturnTime", obj.ReturnTime);
                
                return _dapper.ExecuteGet<T>("Proc_AddBooking", dynamicParameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public T CheckLoginType<T>(account obj)
        {
            try
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("UserId", obj.LoginId);
                dynamicParameters.Add("password", obj.password);                
                return _dapper.ExecuteGet<T>("Proc_CheckLoginType", dynamicParameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public T NewAddBooking<T>(NewBooking obj)
        {
            try
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("ProcId", 1);
                dynamicParameters.Add("Id", obj.Id);
                dynamicParameters.Add("Source", obj.endDest);
                dynamicParameters.Add("Destination", obj.startDest);
                dynamicParameters.Add("TotalPrices", obj.TotalPrices);
                dynamicParameters.Add("Name", obj.fullName);
                dynamicParameters.Add("MobileNo", obj.MobileNo);
                dynamicParameters.Add("PickupDate", obj.rideDate);
                dynamicParameters.Add("PickupTime", obj.rideTime);
                dynamicParameters.Add("ReturnDate", obj.ReturnDate);
                dynamicParameters.Add("ReturnTime", obj.ReturnTime);
                dynamicParameters.Add("passengers", obj.passengers);

                return _dapper.ExecuteGet<T>("Proc_AddBooking", dynamicParameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public T ContactUs<T>(ContactUs obj)
        {
            try
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("ProcId", 1);
                dynamicParameters.Add("Id", obj.Id);
                dynamicParameters.Add("firstname", obj.firstname);
                dynamicParameters.Add("lastname", obj.lastname);
                dynamicParameters.Add("email", obj.email);
                dynamicParameters.Add("phone", obj.phone);
                dynamicParameters.Add("message", obj.message);

                return _dapper.ExecuteGet<T>("Proc_ContactUs", dynamicParameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
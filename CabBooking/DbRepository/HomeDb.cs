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
                
                return _dapper.ExecuteGet<T>("Proc_AddBooking", dynamicParameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
using CabBooking.DBContext;
using CabBooking.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
    }
}
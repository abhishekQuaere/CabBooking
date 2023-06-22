using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace CabBooking.DBContext
{
    public class DapperDbContext : IDisposable
    {
        private string _ConnectionString = null;

        public DapperDbContext()
        {
            _ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["db"].ToString();
        }

        public DbConnection GetDbConnection()
        {
            return new SqlConnection(_ConnectionString);
        }

        public T ExecuteGet<T>(string sp, DynamicParameters dynamicParameters, CommandType commandType = CommandType.StoredProcedure)
        {
            using (IDbConnection con = GetDbConnection())
            {
                return con.Query<T>(sp, dynamicParameters, commandType: commandType).FirstOrDefault();
            }
        }

        public List<T> ExecuteGetAll<T>(string sp, DynamicParameters dynamicParameters, CommandType commandType = CommandType.StoredProcedure)
        {
            using (IDbConnection con = GetDbConnection())
            {
                return con.Query<T>(sp, dynamicParameters, commandType: commandType).ToList();
            }
        }

        public T Get<T>(string sp, DynamicParameters dynamicParameters, CommandType commandType = CommandType.StoredProcedure)
        {
            using (IDbConnection con = GetDbConnection())
            {
                return con.Query<T>(sp, dynamicParameters, commandType: commandType).FirstOrDefault();
            }
        }

        public List<T> GetAll<T>(string sp, DynamicParameters dynamicParameters, CommandType commandType = CommandType.StoredProcedure)
        {
            using (IDbConnection con = GetDbConnection())
            {
                return con.Query<T>(sp, dynamicParameters, commandType: commandType).ToList();
            }
        }

        public T Execute<T>(string sp, DynamicParameters dynamicParameters, CommandType commandType = CommandType.StoredProcedure)
        {
            using (IDbConnection con = GetDbConnection())
            {
                return con.Query<T>(sp, dynamicParameters, commandType: commandType).FirstOrDefault();
            }
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
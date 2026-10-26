using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace AWSFormBuilder.Areas.FormBuilder.DAL
{
    public class DBAccess
    {
        private readonly string _connectionString;

        // MODIFIED - still works exactly as before: no-arg call uses "FormsConnectionString"
        public DBAccess() : this("FormsConnectionString")
        {
        }

        // ADDED - pass a connection string key from appsettings.json, e.g. new DBAccess("FormsConnection")
        public DBAccess(string connectionName)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            _connectionString = configuration.GetConnectionString(connectionName)
                ?? throw new Exception(connectionName + " not found in appsettings.json");
        }

        // MODIFIED - still works exactly as before: uses "FormsConnectionString"
        public DBAccess(IConfiguration configuration) : this(configuration, "FormsConnectionString")
        {
        }

        // ADDED - DI-injected IConfiguration + a specific connection string key
        public DBAccess(IConfiguration configuration, string connectionName)
        {
            _connectionString = configuration.GetConnectionString(connectionName)
                ?? throw new Exception(connectionName + " not found in appsettings.json");
        }

        public DataTable GetDataTable(string storedProcedureName, ref SqlParameter[] sqlParams)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cnn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(storedProcedureName, cnn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (sqlParams != null)
                    cmd.Parameters.AddRange(sqlParams);

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cnn.Open();
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public DataTable GetDataTableNoParm(string strStoredProcedureName)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cnn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(strStoredProcedureName, cnn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cnn.Open();
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public DataSet GetDataSet(string strStoredProcedureName, ref SqlParameter[] sqlParams)
        {
            DataSet ds = new DataSet();
            using (SqlConnection cnn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(strStoredProcedureName, cnn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (sqlParams != null)
                    cmd.Parameters.AddRange(sqlParams);

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cnn.Open();
                    da.Fill(ds);
                }
            }
            return ds;
        }

        public long GetValue(string strStoredProcedureName, ref SqlParameter[] sqlParams)
        {
            long i = 0;
            using (SqlConnection cnn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(strStoredProcedureName, cnn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (sqlParams != null)
                    cmd.Parameters.AddRange(sqlParams);

                cnn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        var obj = dr[0];
                        i = obj != DBNull.Value ? Convert.ToInt64(obj) : 0;
                    }
                }
            }
            return i;
        }

        public object SP_ExecuteScalar(string strStoredProcedureName, ref SqlParameter[] sqlParams)
        {
            using (SqlConnection cnn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(strStoredProcedureName, cnn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 450;
                if (sqlParams != null)
                    cmd.Parameters.AddRange(sqlParams);

                cnn.Open();
                return cmd.ExecuteScalar();
            }
        }

        public void Sp_GetDataReader(out SqlDataReader sqldr, string strStoredProcedureName, ref SqlParameter[] sqlParams)
        {
            SqlConnection cnn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(strStoredProcedureName, cnn)
            {
                CommandType = CommandType.StoredProcedure
            };
            if (sqlParams != null)
                cmd.Parameters.AddRange(sqlParams);

            cnn.Open();
            sqldr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public object GetObject(ref SqlParameter[] sqlParams)
        {
            object obj = null;
            using (SqlConnection cnn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("", cnn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (sqlParams != null)
                    cmd.Parameters.AddRange(sqlParams);

                cnn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                        obj = dr[0];
                }
            }
            return obj;
        }
    }
}

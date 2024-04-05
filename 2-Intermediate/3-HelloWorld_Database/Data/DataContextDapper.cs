using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;


namespace HelloWorld.Data 
{
    public class DataContextDapper
    {
        
        string _sql_connection = "Server=localhost; Database=DotNetCourseDatabase; TrustServerCertificate=true; Trusted_Connection=false; User Id=sa; Password=SQLConnect1;";

        // use connection strying in appsettings.json 
        public DataContextDapper(IConfiguration config) 
        {
            _sql_connection = config.GetConnectionString("DefaultConnection");
        }
        // db connection using dapper 

        public IEnumerable<T> LoadData<T>(string sql) 
        {
            IDbConnection dbConnection = new SqlConnection(_sql_connection);
            return dbConnection.Query<T>(sql);
        }

        public T LoadDataSingle<T>(string sql) 
        {
            IDbConnection dbConnection = new SqlConnection(_sql_connection);
            return dbConnection.QuerySingle<T>(sql);
        }

        public bool ExecuteSql(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(_sql_connection);
            return (dbConnection.Execute(sql) > 0);
        }

        public int ExecuteSqlWithRowCount(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(_sql_connection);
            return dbConnection.Execute(sql);
        }
    }
}
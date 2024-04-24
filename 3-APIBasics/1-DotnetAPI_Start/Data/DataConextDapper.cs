using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace DotnetAPI
{
    class DataContextDapper 
    {
        private readonly IConfiguration _config;
        public IDbConnection dbConnection;

        public DataContextDapper(IConfiguration config)
        {
            _config = config;
            dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
        }

        public IEnumerable<T> LoadData<T>(string sql)
        {
            return dbConnection.Query<T>(sql);
        }

        public T LoadDataSingle<T>(string sql)
        {
            return dbConnection.QuerySingle<T>(sql);
        }

        public bool ExecuteSql(string sql)
        {
            // greater than 0 means true 
            return dbConnection.Execute(sql) > 0;
        }

        public int ExecuteSqlWithRowCount(string sql)
        {
            return dbConnection.Execute(sql);
        }
    }
}
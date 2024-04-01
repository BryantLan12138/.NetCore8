using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;


namespace HelloWorld.Data 
{
    public class DataContextDapper
    {
        // db connection using dapper 
        string _sql_connection = "Server=localhost; Database=DotNetCourseDatabase; TrustServerCertificate=true; Trusted_Connection=false; User Id=sa; Password=SQLConnect1;";

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
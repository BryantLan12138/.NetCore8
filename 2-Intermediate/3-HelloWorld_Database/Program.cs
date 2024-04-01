using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using HelloWorld.Models;
using HelloWorld.Data;


namespace HelloWorld 
{
    public class Program 
    {
        // dapper
        // public static void Main(string[] args) 
        // {
        //     DataContextDapper dapper = new DataContextDapper();

        //     string sql_query = "Select GETDATE();";

        //     DateTime rightNow = dapper.LoadDataSingle<DateTime>(sql_query);

        //     Console.WriteLine(rightNow);

        //     // practicing execute() instead of query 
        //     Computer myComputer = new Computer() 
        //     {
        //         Motherboard = "Z690",
        //         HasWifi = true,
        //         HasLTE = false,
        //         ReleaseDate = DateTime.Now,
        //         Price = 943.87m,
        //         VideoCard = "RTX 4060"
        //     };      

        //     string mutation = @"INSERT INTO TutorialAppSchema.Computer (
        //         Motherboard,
        //         CPUCores,
        //         HasWifi,
        //         HasLTE,
        //         ReleaseDate,
        //         Price,
        //         VideoCard
        //     ) VALUES (
        //         "+"'" + myComputer.Motherboard
        //         + "','" + "12"
        //         + "','" + myComputer.HasWifi
        //         + "','" + myComputer.HasLTE
        //         + "','" + myComputer.ReleaseDate.ToString("yyyy-MM-dd")
        //         + "','" + myComputer.Price
        //         + "','" + myComputer.VideoCard
        //     + "')";

        //     // Console.WriteLine(mutation);
        //     bool result = dapper.ExecuteSql(mutation);
        //     // Console.WriteLine(result);

        //     string get_all = "Select * From TutorialAppSchema.Computer;";
        //     IEnumerable<Computer> sec_result = dapper.LoadData<Computer>(get_all);
        //     foreach(Computer myComputerr in sec_result) {
        //         Console.WriteLine("'" + myComputerr.Motherboard
        //         + "','" + myComputerr.CPUCores
        //         + "','" + myComputerr.HasWifi
        //         + "','" + myComputerr.HasLTE
        //         + "','" + myComputerr.ReleaseDate.ToString("yyyy-MM-dd")
        //         + "','" + myComputerr.Price
        //         + "','" + myComputerr.VideoCard);
        //     }
        // }


        // entityframework
        public static void Main(string[] args) 
        {
            DataContextEF entityFramework = new DataContextEF();

            Computer myComputer = new Computer() 
            {
                Motherboard = "B450",
                HasWifi = true,
                HasLTE = false,
                ReleaseDate = DateTime.Now,
                Price = 1499.0m,
                VideoCard = "RTX 4060"
            };    

            // entityFramework.Add(myComputer);
            entityFramework.SaveChanges();

            IEnumerable<Computer>? computers = entityFramework.Computer?.ToList<Computer>();
            
            if(computers != null )
            {
                foreach(Computer myComputerr in computers) {
                    Console.WriteLine("'" + myComputerr.Motherboard
                    + "','" + myComputerr.CPUCores
                    + "','" + myComputerr.HasWifi
                    + "','" + myComputerr.HasLTE
                    + "','" + myComputerr.ReleaseDate.ToString("yyyy-MM-dd")
                    + "','" + myComputerr.Price
                    + "','" + myComputerr.VideoCard);
                }
            }
            
        }
    }
}
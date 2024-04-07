using System;
using System.Data;
using System.Globalization;
using System.Text.Json;
using System.Text.RegularExpressions;
using Dapper;
using HelloWorld.Data;
using HelloWorld.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using AutoMapper;


namespace HelloWorld
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            DataContextDapper dapper = new DataContextDapper(config);

            // string sql = @"INSERT INTO TutorialAppSchema.Computer (
            //     Motherboard,
            //     HasWifi,
            //     HasLTE,
            //     ReleaseDate,
            //     Price,
            //     VideoCard
            // ) VALUES ('" + myComputer.Motherboard
            //         + "','" + myComputer.HasWifi
            //         + "','" + myComputer.HasLTE
            //         + "','" + myComputer.ReleaseDate
            //         + "','" + myComputer.Price
            //         + "','" + myComputer.VideoCard
            // + "')";

            // File.WriteAllText("log.txt", "\n" + sql + "\n");

            // using StreamWriter openFile = new("log.txt", append: true);

            // openFile.WriteLine("\n" + sql + "\n");

            // openFile.Close();

            string computersJson = File.ReadAllText("ComputersSnake.json");

            
            // Console.WriteLine(computersJson);

            // JsonSerializerOptions options = new JsonSerializerOptions()
            // {
            //     PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            // };

            // IEnumerable<Computer>? computersNewtonSoft = JsonConvert.DeserializeObject<IEnumerable<Computer>>(computersJson);

            IEnumerable<Computer>? computersSystem = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Computer>>(computersJson);

            if(computersSystem != null) 
            {
                // foreach(Computer computer in computersSystem)
                // {
                    Console.WriteLine("Json Property Count: " + computersSystem.Count());
                // }
            }

            /* using the automapper for Models mapping */

            Mapper mapper = new Mapper(new MapperConfiguration((cfg) => {
                cfg.CreateMap<ComputerSnake, Computer>()
                    .ForMember(destionation => destionation.ComputerId, options => 
                        options.MapFrom(source => source.computer_id))
                    .ForMember(destionation => destionation.Motherboard, options => 
                        options.MapFrom(source => source.motherboard))
                    .ForMember(destionation => destionation.CPUCores, options => 
                        options.MapFrom(source => source.cpu_cores))
                    .ForMember(destionation => destionation.HasWifi, options => 
                        options.MapFrom(source => source.has_wifi))
                    .ForMember(destionation => destionation.HasLTE, options => 
                        options.MapFrom(source => source.has_lte))
                    .ForMember(destionation => destionation.ReleaseDate, options => 
                        options.MapFrom(source => source.release_date))
                    .ForMember(destionation => destionation.Price, options => 
                        options.MapFrom(source => source.price))
                    .ForMember(destionation => destionation.VideoCard, options => 
                        options.MapFrom(source => source.video_card));
            }));

            IEnumerable<ComputerSnake>? computersSystemAutomapper = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<ComputerSnake>>(computersJson);

            if(computersSystem != null) 
            {
                IEnumerable<Computer> computerResult = mapper.Map<IEnumerable<Computer>>(computersSystemAutomapper);

                if (computerResult != null)
                {
                    // foreach (Computer computer in computerResult)
                    // {
                        Console.WriteLine("AutoMapper Count: " + computerResult.Count());

                        // string sql = @"INSERT INTO TutorialAppSchema.Computer (
                        //     Motherboard,
                        //     HasWifi,
                        //     HasLTE,
                        //     ReleaseDate,
                        //     Price,
                        //     VideoCard
                        // ) VALUES ('" + EscapeSingleQuote(computer.Motherboard)
                        //         + "','" + computer.HasWifi
                        //         + "','" + computer.HasLTE
                        //         + "','" + computer.ReleaseDate?.ToString("yyyy-MM-dd")
                        //         + "','" + computer.Price.ToString("0.00", CultureInfo.InvariantCulture)
                        //         + "','" + EscapeSingleQuote(computer.VideoCard)
                        // + "')";
                        
                        // dapper.ExecuteSQL(sql);
                    // }
                }
            }


            

            // JsonSerializerSettings settings = new JsonSerializerSettings()
            // {
            //     ContractResolver = new CamelCasePropertyNamesContractResolver()
            // };

            // string computersCopyNewtonsoft = JsonConvert.SerializeObject(computersNewtonSoft, settings);

            // File.WriteAllText("computersCopyNewtonsoft.txt", computersCopyNewtonsoft);

            // string computersCopySystem = System.Text.Json.JsonSerializer.Serialize(computersSystem, options);

            // File.WriteAllText("computersCopySystem.txt", computersCopySystem);

        }

        static string EscapeSingleQuote(string input)
        {
            string output = input.Replace("'", "''");

            return output;
        }

    }
}
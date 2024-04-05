using Microsoft.EntityFrameworkCore;
using HelloWorld.Models;
using Microsoft.Extensions.Configuration;


namespace HelloWorld.Data 
{
    public class DataContextEF : DbContext
    {
        string _sql_connection = "Server=localhost; Database=DotNetCourseDatabase; TrustServerCertificate=true; Trusted_Connection=false; User Id=sa; Password=SQLConnect1;";
        public DataContextEF(IConfiguration config) 
        {
            _sql_connection = config.GetConnectionString("DefaultConnection");
        }
        public DbSet<Computer>? Computer { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if(!options.IsConfigured) 
            {
                options.UseSqlServer(_sql_connection,
                    options => options.EnableRetryOnFailure());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("TutorialAppSchema");
            modelBuilder.Entity<Computer>();
            
            // alternative
            /*
            modelBuilder.Entity<Computer>().
                ToTable("Computer", "TutorialAppSchema");
            */
        }

    }
}
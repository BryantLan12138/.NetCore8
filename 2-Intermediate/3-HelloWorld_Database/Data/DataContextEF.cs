using Microsoft.EntityFrameworkCore;
using HelloWorld.Models;

namespace HelloWorld.Data 
{
    public class DataContextEF : DbContext
    {
        public DbSet<Computer>? Computer { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if(!options.IsConfigured) 
            {
                options.UseSqlServer("Server=localhost; Database=DotNetCourseDatabase; TrustServerCertificate=true; Trusted_Connection=false; User Id=sa; Password=SQLConnect1;",
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
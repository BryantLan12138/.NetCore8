using Microsoft.EntityFrameworkCore;
using DotnetAPI.Models;

namespace DotnetAPI.Data 
{
    public class DataContextEF : DbContext
    {
        private readonly IConfiguration _config;

        // get the connection string of the database 
        public DataContextEF(IConfiguration config)
        {
            _config = config; 
        }

        // use DbSet to map Model to Tables 
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserSalary> UserSalary { get; set; }
        public virtual DbSet<UserJobInfo> UserJobInfo { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(_config.GetConnectionString("DefaultConnection"),
                        // in case network flip 
                        optionsBuilder => optionsBuilder.EnableRetryOnFailure());
            }
        }

        // access to schema 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("TutorialAppSchema");

            // map/align model to table, only this need ToTable since the model and table is not exactly align by itself 
            modelBuilder.Entity<User>()
                .ToTable("Users", "TutorialAppSchema")
                .HasKey(u => u.UserId);

            modelBuilder.Entity<UserSalary>()
                .HasKey(u => u.UserId);

            modelBuilder.Entity<UserJobInfo>()
                .HasKey(u => u.UserId);
        }


    }
}
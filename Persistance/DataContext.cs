using System;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace DbUtils
{
    public class DataContext : DbContext
    {
        public string ConnectionString { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }
        }
        //setup all the tables
        public DbSet<User> Users { get; set; }
        public DbSet<ProjectTask> Tasks { get; set; }
        public DbSet<TasksPerProject> TasksPerProject { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<Status> Statuses { get; set; }

        public DataContext(string connectionString)
        {
            ConnectionString = connectionString;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if (string.IsNullOrEmpty(ConnectionString))
            {
                optionsBuilder.UseSqlServer();
            }
            else
            {
                optionsBuilder.UseSqlServer(ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // TasksPerProject is a table to handle many to many relationship between projects and tasks
            modelBuilder.Entity<TasksPerProject>().HasKey(tp => new { tp.ProjectId, tp.TaskId});
        }





    }
    
}
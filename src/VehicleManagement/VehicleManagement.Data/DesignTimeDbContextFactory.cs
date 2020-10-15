using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;
using VehicleManagement.AppConfig;

namespace VehicleManagement.Data
{
    public abstract class DesignTimeDbContextFactory<TContext> : 
        IDesignTimeDbContextFactory<TContext> where TContext: DbContext
    { 

        public TContext CreateDbContext(string[] args)
        {
            IConfig Iconfig = new Config(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));
            return Create(Iconfig.ConnectionString);
        }
        protected abstract TContext CreateNewInstance(DbContextOptions<TContext> options);
        private TContext Create(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException($"{nameof(connectionString)} is null or empty", nameof(connectionString));

            var optionsBuilder = new DbContextOptionsBuilder<TContext>();
            optionsBuilder.UseSqlite(connectionString);

            var options = optionsBuilder.Options;
            return CreateNewInstance(options);
        }
    }
}

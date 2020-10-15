using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Microsoft.Data.Sqlite;
using VehicleManagement.AppConfig;
using VehicleManagement.Data;
using VehicleManagement.Data.Repository.Implementation;
using VehicleManagement.Data.Repository.Interface;
using VehicleManagement.Util.Mapper;

namespace VehicleManagement.Util.DiConfig
{
    public static class DiConfig
    {
        private static SqliteConnection _connection;
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services.AddScoped<ICarRepository, CarRepository>();
                
        }
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, 
            Config config)
        {

            return services
                .AddScoped<IConfig, Config>((serviceProvider) => config)
                .AddScoped<VehicleDbContext>(options =>
                    {
                        if (_connection == null || _connection.State != ConnectionState.Open)
                        {
                            _connection = new SqliteConnection(config.ConnectionString);
                            _connection.CreateFunction("newid", () => Guid.NewGuid());
                            _connection.Open();
                        }

                        //options.UseSqlite(_connection);
                        var o = new DbContextOptionsBuilder<VehicleDbContext>().UseSqlite(_connection).Options;
                        var context =  new VehicleDbContext(o);
                        context.Database.EnsureCreated();
                        return context;
                    })
                    // .AddSingleton<VehicleDbContext>(s =>
                // {
                //     var connection = new SqliteConnection(config.ConnectionString);
                //     connection.CreateFunction("newid", () => Guid.NewGuid());
                //     connection.Open();
                //     var optionsBuilder = new DbContextOptionsBuilder<VehicleDbContext>();
                //     optionsBuilder.UseSqlite(config.ConnectionString);
                //     return new VehicleDbContext(optionsBuilder.Options);
                // })
                .AddSingleton((serviceProvider) => new AutoMapperConfiguration().GetMapper());
        }
    }
}

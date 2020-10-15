using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using VehicleManagement.AppConfig;
using VehicleManagement.Data;
using VehicleManagement.Data.Repository.Implementation;
using VehicleManagement.Data.Repository.Interface;
using VehicleManagement.Util.Mapper;

namespace VehicleManagement.Util.DiConfig
{
    public static class DiConfig
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services.AddScoped<ICarRepository, CarRepository>();
                
        }
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, 
            Config config)
        {

            return services
                .AddScoped<IConfig, Config>((serviceProvider) => config)
                .AddDbContext<VehicleDbContext>(s => s.UseSqlite(config.ConnectionString))
                .AddSingleton((serviceProvider) => new AutoMapperConfiguration().GetMapper());
        }
    }
}

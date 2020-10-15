using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace VehicleManagement.AppConfig
{
    public class Config: IConfig
    {
        private readonly IConfigurationRoot _configuration;
        public Config(string env)
        {
            try
            {
                var builder = new ConfigurationBuilder()
               .SetBasePath(Path.Join(AppContext.BaseDirectory, "Config"))
               .AddJsonFile($"appsettings.json", optional: false)
               .AddJsonFile($"appsettings.{env.ToString().ToLowerInvariant()}.json", optional: true)
               .AddEnvironmentVariables();
                _configuration = builder.Build();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Exception: {ex}");
                throw;
            }
            
        }

        public string GetConfig(string[] args) {
            return _configuration[$"{string.Join(":", args)}"];
        }

        public string ConnectionString => GetConfig(new[] { "ConnectionStrings", "Default" });
        public string Environment => GetConfig(new[] { "Environment" });
    }
}

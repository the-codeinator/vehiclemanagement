using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using VehicleManagement.AppConfig;

namespace VehicleManagement.Api.EndToEnd.Tests.Factory
{
    public class ApplicationFactory: WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            var env = "devtest";
            var config = new Config(env);

            builder.ConfigureTestServices(services =>
            {
                services.AddSingleton<IConfig>(config);
            });
        }
    }
}

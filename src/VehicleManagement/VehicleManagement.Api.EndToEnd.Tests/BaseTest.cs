using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using VehicleManagement.Api.EndToEnd.Tests.Factory;
using Xunit;

namespace VehicleManagement.Api.EndToEnd.Tests
{
    public class BaseTest: IClassFixture<ApplicationFactory>
    {
        public readonly HttpClient _httpClient;
        public readonly ApplicationFactory _app;
        public BaseTest(ApplicationFactory app)
        {
            _app = app;
            _httpClient = app.CreateClient();
        }
    }
}

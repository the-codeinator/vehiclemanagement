using FluentAssertions;
using System;
using Xunit;

namespace VehicleManagement.AppConfig.Test
{
    public class ConfigTests
    {
        private readonly IConfig _config;
        private const string ENV = "devtest";
        public ConfigTests()
        {
            _config = new Config(ENV);
        }
        [Fact]
        public void TestConfigSetUp_IsNotNull()
        {
            _config.Should().NotBeNull();
        }

        [Fact]
        public void TestConfig_EnvironmentIsSetCorrectly()
        {
            _config.Environment.Should().Be(ENV);
        }
    }
}

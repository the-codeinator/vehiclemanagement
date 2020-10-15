using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleManagement.AppConfig
{
    public interface IConfig
    {
        string ConnectionString { get; }
        string Environment { get; }
    }
}

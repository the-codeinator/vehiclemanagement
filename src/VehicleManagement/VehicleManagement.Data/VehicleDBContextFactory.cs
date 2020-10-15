using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleManagement.Data
{
    public class VehicleDBContextFactory : DesignTimeDbContextFactory<VehicleDbContext>
    {
        protected override VehicleDbContext CreateNewInstance(DbContextOptions<VehicleDbContext>
            options)
        {
            return new VehicleDbContext(options);
        }
    }
}

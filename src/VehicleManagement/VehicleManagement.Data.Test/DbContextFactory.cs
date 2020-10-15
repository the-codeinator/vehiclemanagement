using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleManagement.Data.Test
{
    public class DbContextFactory
    {
        public DbContextFactory()
        {

        }
        public VehicleDbContext CreateContext(string connectionString)
        {
            var connection = new SqliteConnection(connectionString);
            connection.CreateFunction("newid", () => Guid.NewGuid());
            connection.Open();

            var option = new DbContextOptionsBuilder<VehicleDbContext>()
                .UseSqlite(connection).Options;
            var context = new VehicleDbContext(option);
            if (context != null)
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
            return context;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VehicleManagement.Domain.Entities;

namespace VehicleManagement.Data
{

    public interface IVehicleDbContext
    {

    }

    public class VehicleDbContext : DbContext, IVehicleDbContext
    {
        public DbSet<Car> Cars { get; set; }

        public VehicleDbContext(DbContextOptions<VehicleDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyAllConfigurations<VehicleDbContext>();
        }

        public override int SaveChanges()
        {
            this.AddAuditInfo();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = 
            new CancellationToken())
        {
            this.AddAuditInfo();
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}

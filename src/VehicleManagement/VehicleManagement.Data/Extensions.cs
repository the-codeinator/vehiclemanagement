using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using VehicleManagement.Domain.Entities;

namespace VehicleManagement.Data
{
    public static class Extensions
    {
        public static void AddAuditInfo(this DbContext dbContext)
        {
            var entries = dbContext.ChangeTracker.Entries().Where(e =>
                e.Entity is IEntity && (e.State is EntityState.Added || e.State is EntityState.Modified));

            foreach (var entry in entries)
            {
                if (entry.State is EntityState.Added) ((IEntity)entry.Entity).CreatedAt = DateTime.UtcNow;
                ((IEntity)entry.Entity).UpdatedAt = DateTime.UtcNow;
            }
        }
        public static void ApplyAllConfigurations<TDbContext>(this ModelBuilder modelBuilder)
            where TDbContext : DbContext
        {
            var applyConfigurationMethodInfo = modelBuilder
                .GetType()
                .GetMethods(BindingFlags.Instance | BindingFlags.Public)
                .First(m => m.Name.Equals("ApplyConfiguration", StringComparison.OrdinalIgnoreCase));

            var ret = typeof(TDbContext).Assembly
                .GetTypes()
                .Select(t => (t,
                    i: t.GetInterfaces().FirstOrDefault(i =>
                        i.Name.Equals(typeof(IEntityTypeConfiguration<>).Name, StringComparison.Ordinal))))
                .Where(it => it.i != null)
                .Select(it => (et: it.i.GetGenericArguments()[0], cfgObj: Activator.CreateInstance(it.t)))
                .Select(it =>
                    applyConfigurationMethodInfo.MakeGenericMethod(it.et).Invoke(modelBuilder, new[] { it.cfgObj }))
                .ToList();
        }
    }
}

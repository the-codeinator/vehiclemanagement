using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VehicleManagement.Domain.Entities;

namespace VehicleManagement.Data.Configuration
{
    public class CarConfiguration: IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.ToTable("Car");

            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id).HasColumnName("Id").HasDefaultValueSql("newId()");

            builder.Property(b => b.BodyType).HasColumnName("BodyType").HasMaxLength(40)
                .IsRequired();

            builder.Property(e => e.Brand).HasColumnName("Brand").HasMaxLength(40).IsRequired();

            builder.Property(b => b.Doors).HasColumnName("Doors").IsRequired();

            builder.Property(b => b.Make).HasColumnName("Make").IsRequired();
            builder.Property(e => e.Model).HasColumnName("Model").HasMaxLength(40).IsRequired();
            builder.Property(e => e.Price).HasColumnName("Price").IsRequired();

            builder.Property(e => e.CreatedAt).HasColumnName("CreatedAt").IsRequired();

            builder.Property(e => e.UpdatedAt).HasColumnName("UpdatedAt").IsRequired();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F1_Project.Models.Map
{
    public class DriversMap : IEntityTypeConfiguration<Driver>
    {
        public void Configure(EntityTypeBuilder<Driver> builder)
        {
            builder.Property(d => d.FullName).IsRequired().HasMaxLength(100);
            builder.Property(d => d.Nationality).IsRequired().HasMaxLength(100);
            builder.Property(d => d.BirthDate).IsRequired();

            builder.HasMany(d => d.DriverTeams).WithOne(d => d.Driver);
        }
    }
}
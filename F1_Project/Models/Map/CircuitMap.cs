using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F1_Project.Models.Map
{
    public class CircuitMap : IEntityTypeConfiguration<Circuit>
    {
        public void Configure(EntityTypeBuilder<Circuit> builder)
        {
            builder.Property(c => c.FullName).IsRequired().HasMaxLength(100);
            builder.Property(c => c.CircuitLength).IsRequired();
            builder.Property(c => c.NumberLaps).IsRequired();

            builder.HasMany(c => c.Rounds).WithOne(c => c.Circuit);
        }
    }
}
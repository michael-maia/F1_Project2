using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F1_Project.Models.Map
{
    public class TeamMap : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.Property(t => t.FullName).IsRequired().HasMaxLength(100);
            builder.Property(t => t.Nationality).IsRequired().HasMaxLength(100);

            builder.HasMany(t => t.DriverTeams).WithOne(t => t.Team);
            builder.HasMany(t => t.ChampionshipTeams).WithOne(t => t.Team);
        }
    }
}
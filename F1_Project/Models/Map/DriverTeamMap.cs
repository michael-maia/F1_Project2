using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F1_Project.Models.Map
{
    public class DriverTeamMap : IEntityTypeConfiguration<DriverTeam>
    {
        public void Configure(EntityTypeBuilder<DriverTeam> builder)
        {
            builder.HasKey(dt => new { dt.DriverId, dt.TeamId });
            builder.HasOne(dt => dt.Driver).WithMany(dt => dt.DriverTeams).HasForeignKey(dt => dt.DriverId);
            builder.HasOne(dt => dt.Team).WithMany(dt => dt.DriverTeams).HasForeignKey(dt => dt.TeamId);
        }
    }
}

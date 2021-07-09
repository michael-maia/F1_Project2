using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F1_Project.Models.Map
{
    public class ChampionshipTeamMap : IEntityTypeConfiguration<ChampionshipTeam>
    {
        public void Configure(EntityTypeBuilder<ChampionshipTeam> builder)
        {
            builder.HasKey(dt => new { dt.ChampionshipId, dt.TeamId });
            builder.HasOne(dt => dt.Championship).WithMany(dt => dt.ChampionshipTeams).HasForeignKey(dt => dt.ChampionshipId);
            builder.HasOne(dt => dt.Team).WithMany(dt => dt.ChampionshipTeams).HasForeignKey(dt => dt.TeamId);
        }
    }
}

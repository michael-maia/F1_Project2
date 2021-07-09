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
            builder.HasKey(ct => new { ct.ChampionshipId, ct.TeamId });
            builder.HasOne(ct => ct.Championship).WithMany(ct => ct.ChampionshipTeams).HasForeignKey(ct => ct.ChampionshipId);
            builder.HasOne(ct => ct.Team).WithMany(ct => ct.ChampionshipTeams).HasForeignKey(ct => ct.TeamId);
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F1_Project.Models.Map
{
    public class ChampionshipMap : IEntityTypeConfiguration<Championship>
    {
        public void Configure(EntityTypeBuilder<Championship> builder)
        {
            builder.Property(c => c.Year).IsRequired();

            builder.HasMany(c => c.ChampionshipTeams).WithOne(c => c.Championship);
            builder.HasMany(c => c.Rounds).WithOne(c => c.Championship);
        }
    }
}
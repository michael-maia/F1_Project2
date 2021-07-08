using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F1_Project.Models.Map
{
    public class RoundMap : IEntityTypeConfiguration<Round>
    {
        public void Configure(EntityTypeBuilder<Round> builder)
        {
            builder.Property(r => r.Number).IsRequired();

            builder.HasOne(r => r.Championship).WithMany(r => r.Rounds).HasForeignKey(r => r.ChampionshipId);
        }
    }
}

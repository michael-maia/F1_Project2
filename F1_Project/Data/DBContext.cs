using F1_Project.Models;
using F1_Project.Models.Map;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F1_Project.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options) { }

        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Circuit> Circuits { get; set; }
        public DbSet<Championship> Championships { get; set; }
        public DbSet<Round> Rounds { get; set; }
        public DbSet<DriverTeam> DriverTeams { get; set; }
        public DbSet<ChampionshipTeam> ChampionshipTeams { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new RoundMap());
            builder.ApplyConfiguration(new ChampionshipMap());
            builder.ApplyConfiguration(new CircuitMap());
            builder.ApplyConfiguration(new DriverMap());
            builder.ApplyConfiguration(new TeamMap());
            builder.ApplyConfiguration(new ChampionshipTeamMap());
            builder.ApplyConfiguration(new DriverTeamMap());
        }
    }
}
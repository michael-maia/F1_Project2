using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F1_Project.Models
{
    public class Driver
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Nationality { get; set; }
        public DateTime BirthDate { get; set; }
        public int CarNumber { get; set; }
        public int ChampionshipsVictories { get; set; }
        public int RaceVictories { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }

        // Chaves estrangeiras
        public ICollection<DriverTeam> DriverTeams { get; set; }
    }
}
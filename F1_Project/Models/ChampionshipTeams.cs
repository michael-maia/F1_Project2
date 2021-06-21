using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F1_Project.Models
{
    public class ChampionshipTeams
    {
        public int Id { get; set; }

        // Chaves estrangeiras
        public int ChampionshipId { get; set; }
        public Championship Championship { get; set; }
        public int TeamId { get; set; }
        public Team Team { get; set; }
    }
}
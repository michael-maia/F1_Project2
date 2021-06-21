using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F1_Project.Models
{
    public class Round
    {
        public int Id { get; set; }        
        public int Number { get; set; }

        // Chaves estrangeiras
        public int ChampionshipId { get; set; }
        public Championship Championship { get; set; }
        public int CircuitId { get; set; }
        public Circuit Circuit { get; set; }
        public int DriverId { get; set; }
        public Driver WinningDriver { get; set; }
        public int TeamId { get; set; }
        public Team WinningTeam { get; set; }
    }
}
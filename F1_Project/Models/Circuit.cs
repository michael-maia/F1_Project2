using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F1_Project.Models
{
    public class Circuit
    {
        public int Id { get; set; }
        public string FullName { get; set; }        
        public int NumberRacesHeld { get; set; }
        public double CircuitLength { get; set; }
        public int NumberLaps { get; set; }
        public DateTime YearFirstRace { get; set; }
        public DateTime LapRecord { get; set; }
        public string Description { get; set; }

        // Chaves estrangeiras
        public int DriverId { get; set; }
        public Driver DriverMostWins { get; set; }
        public int TeamId { get; set; }
        public Team TeamMostWins { get; set; }
        public ICollection<Round> Rounds { get; set; }
        // PROPRIEDADES PARA LISTA DE FOTOS
    }
}
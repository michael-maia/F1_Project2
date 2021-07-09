using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [DataType(DataType.Date)]
        public DateTime YearFirstRace { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0: mm:ss.fff}")]
        public DateTime LapRecord { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }

        // Chaves estrangeiras
        public ICollection<Round> Rounds { get; set; }
        //public int DriverId { get; set; }
        //public Driver DriverMostWins { get; set; }
        //public int TeamId { get; set; }
        //public Team TeamMostWins { get; set; }   
    }
}
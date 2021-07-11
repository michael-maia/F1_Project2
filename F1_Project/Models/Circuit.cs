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

        [Required(ErrorMessage = "This field can't be empty")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "This field must have more than 5 chars")]
        public string FullName { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "This field cant be less or equal than zero")]
        public int NumberRacesHeld { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "This field cant be less or equal than zero")]
        public double CircuitLength { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "This field cant be less or equal than zero")]
        public int NumberLaps { get; set; }

        [DataType(DataType.Date)]
        public DateTime YearFirstRace { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:mm:ss.fff}", ApplyFormatInEditMode = true)]
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
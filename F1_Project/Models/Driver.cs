using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace F1_Project.Models
{
    public class Driver
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "This field can't be empty")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "This field can't be empty")]
        public string Nationality { get; set; }

        [Required(ErrorMessage = "This field can't be empty")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }        
        public int CarNumber { get; set; }

        [DefaultValue(0)]
        public int ChampionshipsVictories { get; set; }

        [DefaultValue(0)]
        public int RaceVictories { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }

        // Chaves estrangeiras
        public ICollection<DriverTeam> DriverTeams { get; set; }
    }
}
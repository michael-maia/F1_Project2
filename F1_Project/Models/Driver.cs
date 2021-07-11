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
        [StringLength(100, MinimumLength = 5, ErrorMessage = "This field must have more than 5 chars")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "This field can't be empty")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "This field must have more than 5 chars")]
        public string Nationality { get; set; }

        [Required(ErrorMessage = "This field can't be empty")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "This field cant be less or equal than zero")]
        public int ChampionshipsVictories { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "This field cant be less or equal than zero")]
        public int RaceVictories { get; set; }

        [Range(1, 99, ErrorMessage = "This field cant be less or equal than zero")]
        public int CarNumber { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }

        // Chaves estrangeiras
        public ICollection<DriverTeam> DriverTeams { get; set; }
    }
}
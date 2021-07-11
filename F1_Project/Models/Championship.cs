using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace F1_Project.Models
{
    public class Championship
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "This field can't be empty")]
        [Range(1, int.MaxValue,ErrorMessage = "This field cant be less or equal than zero")]
        public int Year { get; set; }

        // Chaves estrangeiras
        public ICollection<ChampionshipTeam> ChampionshipTeams { get; set; }
        public ICollection<Round> Rounds { get; set; }       
    }
}
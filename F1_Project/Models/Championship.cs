using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F1_Project.Models
{
    public class Championship
    {
        public int Id { get; set; }
        public int Year { get; set; }

        // Chaves estrangeiras
        public ICollection<ChampionshipTeam> ChampionshipTeams { get; set; }
        public ICollection<Round> Rounds { get; set; }       
    }
}
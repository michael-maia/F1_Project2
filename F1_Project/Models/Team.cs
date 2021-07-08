﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F1_Project.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Nationality { get; set; }        
        public int RaceVictories { get; set; }
        public int TeamsChampionshipsVictories { get; set; }
        public int DriversChampionshipsVictories { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }

        // Chaves estrangeiras
        public ICollection<DriverTeams> DriverTeams { get; set; }
        public ICollection<ChampionshipTeams> ChampionshipTeams { get; set; }
    }
}
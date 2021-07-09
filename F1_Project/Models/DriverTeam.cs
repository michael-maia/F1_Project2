using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F1_Project.Models
{
    public class DriverTeam
    {
        public int Id { get; set; }
        public int InitialYear { get; set; }
        public int FinalYear { get; set; }

        // Chaves estrangeiras
        public int DriverId { get; set; }
        public Driver Driver { get; set; }

        public int TeamId { get; set; }
        public Team Team { get; set; }        
    }
}
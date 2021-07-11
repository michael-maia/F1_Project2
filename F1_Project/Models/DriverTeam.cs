using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace F1_Project.Models
{
    public class DriverTeam
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "This field can't be empty")]
        [Range(1, int.MaxValue, ErrorMessage = "This field cant be less or equal than zero")]
        public int InitialYear { get; set; }

        [Required(ErrorMessage = "This field can't be empty")]
        [Range(1, int.MaxValue, ErrorMessage = "This field cant be less or equal than zero")]
        public int FinalYear { get; set; }

        // Chaves estrangeiras
        public int DriverId { get; set; }
        public Driver Driver { get; set; }

        public int TeamId { get; set; }
        public Team Team { get; set; }        
    }
}
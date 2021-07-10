using F1_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F1_Project.Models.ViewModel
{
    public class DriverIndexData
    {
        public IEnumerable<Driver> Drivers { get; set; }
        public IEnumerable<Team> Teams { get; set; }
        public IEnumerable<Championship> Championships { get; set; }
    }
}

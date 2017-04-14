using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWorld.ViewModels
{
    public class TripViewModel
    {
        //ATTRIBUTES - Validation;
        [Required] //bring in using System.ComponentModel.DataAnnotations;
        [StringLength(100,MinimumLength =5)]
        //PROPERTIES
        public string Name { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow; //use as when this was made unless someone overwrites it (UTC-Universal Time Zone)
    }
}

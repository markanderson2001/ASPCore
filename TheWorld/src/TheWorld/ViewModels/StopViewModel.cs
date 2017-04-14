using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWorld.ViewModels
{
    public class StopViewModel
    {

        //ATTRIBUTES /VALIDAION
        [Required]
        [StringLength(100,MinimumLength =5)]
        public string Name { get; set; }
                //not required for now - more computed values
        public Double Latitude { get; set; }
        public Double Longitude { get; set; }
        [Required]
        public int Order { get; set; }
        [Required]
        public DateTime Arrival { get; set; }
    }
}

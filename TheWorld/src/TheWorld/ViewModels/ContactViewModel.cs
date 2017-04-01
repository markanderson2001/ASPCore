using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWorld.ViewModels
{
    //Porpose to Communicate with the contact page and retrieve data - by using a class we are using model binding, to allow us to accepts the data from that form directly inside of  our controller directly inside of a method of our controller
    public class ContactViewModel
    {
        //1st validation - fields HAS to be completed - [required
        //2nd validation - tell it it is an email ddress [emailaddress]
        //3nd validation - [Stringlength] max and min
        // ** we want to inform user of validation -add new librarie for validation in bower.json 

        [Required] 
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(4096,MinimumLength =10)]
        public string Message { get; set; }
    }
}

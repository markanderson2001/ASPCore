using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWorld.ViewModels
{
    //Purpose to Communicate with the contact page and retrieve data - by using a class we are using model binding, 
    //to allow us to accept data from that form directly inside a method of our controller
    public class ContactViewModel
    {
        //1st validation - fields HAS to be completed - [required
        //2nd validation - tell it it is an email ddress [emailaddress]
        //3nd validation - [Stringlength] max and min
        // ** we want to inform user of validation -add new library for validation in bower.json 

        //ATTRIBUTES;
        [Required] 
        //PROPERTY;
        public string Name { get; set; }
        //ATTRIBUTES;
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        //ATTRIBUTES;
        [Required]
        [StringLength(4096,MinimumLength =10)]
        public string Message { get; set; }
    }
}

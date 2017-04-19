﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheWorld.ViewModels
{
    public class LoginViewModel
    {
        //add 2 new properties
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}

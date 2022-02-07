﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ATV2_SENAC.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Lembre de mim")]
        public bool RememberMe { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiverpoolWebsite.BLL.DTOs
{
    public class RegisterDTO
    { 
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
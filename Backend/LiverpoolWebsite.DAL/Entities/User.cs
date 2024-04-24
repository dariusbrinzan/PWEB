﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiverpoolWebsite.DAL.Entities
{
    public class User : IdentityUser<int>
    {
        public string RefreshToken { get; set; }
        // one to many
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}

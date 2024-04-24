using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiverpoolWebsite.DAL.Entities
{
    public class UserRole : IdentityUserRole<int>
    {
        // one to mant
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}

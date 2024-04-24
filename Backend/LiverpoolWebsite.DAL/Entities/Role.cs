using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiverpoolWebsite.DAL.Entities
{
    public class Role : IdentityRole<int>
    {
        // one to many relation
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}

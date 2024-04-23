using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiverpoolWebsite.DAL.Entities
{
    public class Team
    {
        public int TeamId { get; set; }
        public string Name { get; set; }
        public string photoUrl { get; set; }
        public int StadiumId { get; set; }
        public virtual Stadium Stadium {get; set; }
    }
}

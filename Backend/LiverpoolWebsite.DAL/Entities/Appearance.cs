using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiverpoolWebsite.DAL.Entities
{
    public class Appearance
    {
        public int MatchId { get; set; }
        public int PlayerId { get; set; }
        public int Goals { get; set; }
        public int YellowCards { get; set; }
        public bool RedCard { get; set; }
        // one to many relation
        public virtual Player Player { get; set; }
        // one to many relation
        public virtual Match Match { get; set; }
    }
}

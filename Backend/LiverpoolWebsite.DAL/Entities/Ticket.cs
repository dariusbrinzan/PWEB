using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiverpoolWebsite.DAL.Entities
{
    public class Ticket
    {
        public int TicketId { get; set; }
        public int MatchId { get; set; }
        // many to one
        public String Stand { get; set; }
        // many to one relation
        public virtual Match Match { get; set; }
    }
}

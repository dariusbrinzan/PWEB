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
        public String Stand { get; set; }
        public virtual Match Match { get; set; }
    }
}

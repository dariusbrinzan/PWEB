using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiverpoolWebsite.DAL.DTOs
{
    public class TicketDTO
    {
        public int MatchId { get; set; }
        public String Stand { get; set; }
        public int Place { get; set; }
    }
}

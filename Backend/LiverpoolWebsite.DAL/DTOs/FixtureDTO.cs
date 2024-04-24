using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiverpoolWebsite.DAL.DTOs
{
    public class FixtureDTO
    {
        public string MatchInfo { get; set; }
        public string TeamPhoto { get; set; }
        public List<TotalSeatsDTO> Tickets { get; set; }

    }
}

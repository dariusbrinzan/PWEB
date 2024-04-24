using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiverpoolWebsite.DAL.DTOs
{
    public class MatchDTO
    {
        public bool HomeOrAway { get; set; }
        public string Opponent { get; set; }
        public string Stadium { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }
        public string Competition { get; set; }
        public DateTime MatchDate { get; set; }
        public string PhotoUrl { get; set; }

    }
}

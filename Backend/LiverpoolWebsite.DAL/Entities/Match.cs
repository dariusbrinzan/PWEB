using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiverpoolWebsite.DAL.Entities
{
    public class Match
    {
        public int MatchId { get; set; }
        public int TeamId { get; set; }
        public bool HomeOrAway { get; set; }

        private int _goalsFor = -1;
        public int GoalsFor { get { return _goalsFor; }  set { _goalsFor = value; } }
        private int _goalsAgainst = -1;
        public int GoalsAgainst { get { return _goalsAgainst; } set { _goalsAgainst = value; } }
        public int CompetitionId { get; set; }
        public DateTime MatchDate { get; set; }
        public virtual Team Team { get; set; }
        public virtual Competition Competition { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
        public virtual ICollection<Appearance> Appearances { get; set; }
    }
}

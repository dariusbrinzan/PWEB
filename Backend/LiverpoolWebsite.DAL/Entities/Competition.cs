using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiverpoolWebsite.DAL.Entities
{
    public class Competition
    {
        public int CompetitionId { get; set; }
        public string Name { get; set; }
        public ICollection<Match> Matches { get; set; }
    }
}

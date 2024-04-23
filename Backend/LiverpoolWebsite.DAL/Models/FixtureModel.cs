using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiverpoolWebsite.DAL.Models
{
    public class FixtureModel
    {
        public string MatchInfo { get; set; }
        public string TeamPhoto { get; set; }
        public List<TotalSeatsModel> Tickets { get; set; }

    }
}

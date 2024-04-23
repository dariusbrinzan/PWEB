using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiverpoolWebsite.DAL.Entities
{
    public class Stadium
    {
        public int StadiumId { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public virtual Team Team{get; set; }
    }
}

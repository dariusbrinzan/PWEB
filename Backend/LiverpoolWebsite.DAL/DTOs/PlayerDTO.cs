using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiverpoolWebsite.DAL.DTOs
{
    public class PlayerDTO
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Nationality { get; set; }
        public string Position { get; set; }
        public int ShirtNumber { get; set; }
        public string PhotoUrl { get; set; }
        public int App { get; set; }
        public int Goals { get; set; }
    }
}

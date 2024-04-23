using LiverpoolWebsite.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiverpoolWebsite.BLL.Interfaces
{
    public interface ITicketManager
    {
        Task<List<FixtureModel>> GetTickets();
        Task<int> UpdateTickets(int match_id, string oldStand, string newStand, int nr);
        Task<int> DeleteTickets(int match_id, string stand, int nr);
        Task CreateTickets(int match_id, string stand, int nr);

    }
}

using LiverpoolWebsite.DAL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiverpoolWebsite.BLL.Interfaces
{
    public interface ITicketManager
    {
        Task<List<FixtureDTO>> GetTickets();
        Task<int> UpdateTickets(int match_id, string oldStand, string newStand, int nr);
        Task<int> DeleteTickets(int match_id, string stand, int nr);
        Task CreateTickets(int match_id, string stand, int nr);

    }
}

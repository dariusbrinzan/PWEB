using LiverpoolWebsite.DAL.Entities;
using LiverpoolWebsite.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiverpoolWebsite.DAL.Interfaces
{
    public interface ITicketRepository
    {
        Task<List<Match>> GetFutureMatches();
        Task<List<TotalSeatsModel>> GetMatchTicketsByStand(int match_id);
        Task Create(int match_id, string stand, int nr);
        Task<int> Update(int match_id, string oldStand, string newStand, int nr);
        Task<int> Delete(int match_id, string stand, int nr);
    }
}

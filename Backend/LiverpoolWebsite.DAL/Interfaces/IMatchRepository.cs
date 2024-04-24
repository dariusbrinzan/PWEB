using LiverpoolWebsite.DAL.Entities;
using LiverpoolWebsite.DAL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiverpoolWebsite.DAL.Interfaces
{
   public interface IMatchRepository
    {
        Task<List<Match>> GetFutureMatches();
        Task<List<Match>> GetMatchesByComp(string comp);
        Task Create(Match match);
        Task<bool> Update(Match match);
        Task<bool> Delete(int match_id);
    }
}

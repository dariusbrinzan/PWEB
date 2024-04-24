using LiverpoolWebsite.DAL.Entities;
using LiverpoolWebsite.DAL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiverpoolWebsite.BLL.Interfaces
{
    public interface IMatchManager
    {
        Task<List<MatchDTO>> GetMatches(string comp);
        Task CreateMatch(Match match);
        Task<bool> UpdateMatch(Match match);
        Task<bool> DeleteMatch(int match_id);
    }
}

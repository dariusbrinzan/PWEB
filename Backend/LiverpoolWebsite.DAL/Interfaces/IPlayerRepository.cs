using LiverpoolWebsite.DAL.Entities;
using LiverpoolWebsite.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiverpoolWebsite.DAL.Interfaces
{
    public interface IPlayerRepository
    {
        //Task<List<Player>> GetAllPlayers();
        Task<List<Player>> GetPlayersByPosition(string pos);
        Task Create(Player player);
        Task<bool> Update(Player player);
        Task<bool> Delete(int player_id);
    }
}

using LiverpoolWebsite.DAL.Entities;
using LiverpoolWebsite.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiverpoolWebsite.BLL.Interfaces
{
    public interface IPlayerManager
    {
        Task<List<PlayerModel>> GetPlayers(string pos);
        Task CreatePlayer(Player player);
        Task<bool> UpdatePlayer(Player player);
        Task<bool> DeletePlayer(int player_id);
    }
}

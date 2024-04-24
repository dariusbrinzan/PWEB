using LiverpoolWebsite.DAL.Entities;
using LiverpoolWebsite.DAL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiverpoolWebsite.BLL.Interfaces
{
    public interface IPlayerManager
    {
        Task<List<PlayerDTO>> GetPlayers(string pos);
        Task CreatePlayer(Player player);
        Task<bool> UpdatePlayer(Player player);
        Task<bool> DeletePlayer(int player_id);
    }
}

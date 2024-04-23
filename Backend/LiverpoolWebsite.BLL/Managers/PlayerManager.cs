using LiverpoolWebsite.BLL.Interfaces;
using LiverpoolWebsite.DAL.Entities;
using LiverpoolWebsite.DAL.Interfaces;
using LiverpoolWebsite.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiverpoolWebsite.BLL.Managers
{
    public class PlayerManager : IPlayerManager
    {
        private readonly IPlayerRepository _playerRepo;

        public PlayerManager(IPlayerRepository playerRepo)
        {
            _playerRepo = playerRepo;
        }

        // manager care ia toti jucatorii de pe o anumita pozitie
        public async Task<List<PlayerModel>> GetPlayers(string pos)
        {
            var players = await _playerRepo.GetPlayersByPosition(pos);
            var list = new List<PlayerModel>();

            foreach(var player in players)
            {
                var playerModel = new PlayerModel
                {
                    Name = player.Name,
                    Age = (DateTime.Now.Month < player.BirthDate.Month || (DateTime.Now.Month == player.BirthDate.Month && DateTime.Now.Day < player.BirthDate.Day)
                         ? DateTime.Now.Year - player.BirthDate.Year - 1 : DateTime.Now.Year - player.BirthDate.Year),
                    Nationality = player.Nationality,
                    Position = player.Position,
                    ShirtNumber = player.ShirtNumber,
                    PhotoUrl = player.PhotoUrl,
                    App = player.Appearances.Count,
                    Goals = player.Appearances.Sum(x => x.Goals)
                };

                list.Add(playerModel);
            }

            return list;
        }

        public async Task CreatePlayer(Player player)
        {
            await _playerRepo.Create(player);
        }

        public async Task<bool> UpdatePlayer(Player player)
        {
            bool res = await _playerRepo.Update(player);
            return res;
        }

        public async Task<bool> DeletePlayer(int player_id)
        {
            bool res = await _playerRepo.Delete(player_id);
            return res;
        }
    }
}

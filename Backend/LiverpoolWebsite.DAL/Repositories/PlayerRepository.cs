using LiverpoolWebsite.DAL.Entities;
using LiverpoolWebsite.DAL.Interfaces;
using LiverpoolWebsite.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiverpoolWebsite.DAL.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly AppDbContext _context;

        public PlayerRepository(AppDbContext context)
        {
            _context = context;
        }
        // functia creeaza un jucator cu informatiile primite din front-end
        public async Task Create(Player playerModel)
        {
            Player player = new Player
            {
                Name = playerModel.Name,
                Nationality = playerModel.Nationality,
                ShirtNumber = playerModel.ShirtNumber,
                BirthDate = playerModel.BirthDate,
                Position = playerModel.Position
            };

            await _context.Players.AddAsync(player);
            await _context.SaveChangesAsync();
        }

        // functia sterge un jucator cu id-ul primit din front-end
        public async Task<bool> Delete(int player_id)
        {
            var player = await _context.Players.FindAsync(player_id);
            if (player != null)
            {
                _context.Players.Remove(player);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        // functie care ia din baza de date toti jucatorii
      /*  public async Task<List<Player>> GetAllPlayers()
        {
            var players = await (await GetAllQuery()).ToListAsync();
            return players;
        }*/

        // functia care ia din baza de date toti jucatorii care joaca pe postul dat ca parametru
        public async Task<List<Player>> GetPlayersByPosition(string pos)
        {
            var players = await _context.Players.Include(x => x.Appearances).Where(x => x.Position == pos).ToListAsync();
            return players;
        }

        // functia care modifica un jucator, aceeasi problema ca la meci
        public async Task<bool> Update(Player player)
        {
            var playerUp = await _context.Players.FindAsync(player.PlayerId);

            if (playerUp != null)
            {
                var defPlayer = new Player();
                System.Reflection.PropertyInfo[] properties = typeof(Player).GetProperties();
                foreach (var prop in properties)
                {
                    var value = prop.GetValue(player);
                    var valDef = prop.GetValue(defPlayer);
                    if (value != null)
                    {
                        if (valDef == null || value.ToString() != valDef.ToString())
                        {
                            prop.SetValue(playerUp, value);
                        }
                    }
                }

                _context.Players.Update(playerUp);
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }

   /*     private async Task<IQueryable<Player>> GetAllQuery()
        {
            var query = _context.Players.AsQueryable();
            return query;
        }*/
    }
}

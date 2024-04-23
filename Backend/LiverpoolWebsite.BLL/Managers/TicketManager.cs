using LiverpoolWebsite.BLL.Interfaces;
using LiverpoolWebsite.DAL.Interfaces;
using LiverpoolWebsite.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiverpoolWebsite.BLL.Managers
{
    public class TicketManager : ITicketManager
    {
        private readonly ITicketRepository _ticketRepo;
        public TicketManager(ITicketRepository ticketRepo)
        {
            _ticketRepo = ticketRepo;
        }
        public async Task<int> UpdateTickets(int match_id, string oldStand, string newStand, int nr)
        {
            int res = await _ticketRepo.Update(match_id, oldStand, newStand, nr);
            return res;
        }

        public async Task<int> DeleteTickets(int match_id, string stand, int nr)
        {
            int res = await _ticketRepo.Delete(match_id, stand, nr);
            return res;
        }

        public async Task CreateTickets(int match_id, string stand, int nr)
        {
            await _ticketRepo.Create(match_id, stand, nr);
        }

        // manager care pentru fiecare meci nedisputat returneaza
        // informatii: data, stadionul, echipa adversa, numarul de 
        // bilete disponibile pe fiecare stand
        public async Task<List<FixtureModel>> GetTickets()
        {
            var matches = await _ticketRepo.GetFutureMatches();

            var res = new List<FixtureModel>();

            foreach(var match in matches)
            {
                var matchInfo = $"{match.Team.Name}, {(match.HomeOrAway ? "Anfield" : match.Team.Stadium.Name)}, {match.MatchDate}";

                var tick = await _ticketRepo.GetMatchTicketsByStand(match.MatchId);

                var fixt = new FixtureModel { 
                    MatchInfo = matchInfo, 
                    TeamPhoto = match.Team.photoUrl, 
                    Tickets = tick 
                };

                res.Add(fixt);
            }

            return res;
        }
    }
}

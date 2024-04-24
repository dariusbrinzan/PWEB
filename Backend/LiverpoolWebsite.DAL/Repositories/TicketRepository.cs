using LiverpoolWebsite.DAL.Entities;
using LiverpoolWebsite.DAL.Interfaces;
using LiverpoolWebsite.DAL.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiverpoolWebsite.DAL.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly AppDbContext _context;

        public TicketRepository(AppDbContext context)
        {
            _context = context;
        }

        // functia creeaza bilete cu informatiile primite din front-end
        public async Task Create(int match_id, string stand, int nr)
        {

            for (int i = 0; i < nr; ++i)
            {
                Ticket ticket = new Ticket
                {
                    MatchId = match_id,
                    Stand = stand
                };
                await _context.Tickets.AddAsync(ticket);
            }

            await _context.SaveChangesAsync();
        }

        // functia sterge bilete cu datele primite din front-end
        public async Task<int> Delete(int match_id, string stand, int nr)
        {
            var list = _context.Tickets.Select(x => x).Where(x => x.MatchId == match_id && x.Stand == stand).Take(nr).ToList();

            for (int i = 0; i < list.Count(); ++i)
            {
                _context.Tickets.Remove(list[i]);
            }

            await _context.SaveChangesAsync();

            return list.Count();
        }

        // functie care returneaza pentru meciul dat ca parametru o lista cu stand-urile si numarul de bilete
        // disponibile in fiecare stand
        public async Task<List<TotalSeatsDTO>> GetMatchTicketsByStand(int match_id)
        {

            var res = await _context.Tickets.Join(_context.Matches, x => x.MatchId, y => y.MatchId, (x, y) => new { id = x.MatchId, std = x.Stand, mDate = y.MatchDate })
                                      .Where(x => x.id == match_id)
                                      .GroupBy(x => x.std)
                                      .Select(x => new { stand = x.Key, nrTickets = x.Count() })
                                      .ToListAsync();

            var list = new List<TotalSeatsDTO>();

            for (int i = 0; i < res.Count(); ++i)
            {
                var tSeats = new TotalSeatsDTO
                {
                    Stand = res[i].stand,
                    Seats = res[i].nrTickets
                };

                list.Add(tSeats);
            }

            return list;

        }

        // functie care ia dinbaza de date toate meciurile care inca nu s-au disputat
        public async Task<List<Match>> GetFutureMatches()
        {
            var matches = await _context.Matches.Include(x => x.Team).ThenInclude(x => x.Stadium).Include(x => x.Competition).Where(x => x.MatchDate > DateTime.Now).OrderBy(x => x.MatchDate).ToListAsync();
            return matches;
        }

        // functie care modfica bilete cu datele primite din front-end
        public async Task<int> Update(int match_id, string oldStand, string newStand, int nr)
        {
            var list = _context.Tickets.Select(x => x).Where(x => x.MatchId == match_id && x.Stand == oldStand).Take(nr).ToList();

            for (int i = 0; i < list.Count(); ++i)
            {
                list[i].Stand = newStand;
                _context.Tickets.Update(list[i]);
            }

            await _context.SaveChangesAsync();

            return list.Count();
        }
    }
}


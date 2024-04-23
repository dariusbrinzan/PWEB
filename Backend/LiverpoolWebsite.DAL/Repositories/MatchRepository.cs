using LiverpoolWebsite.DAL.Entities;
using LiverpoolWebsite.DAL.Interfaces;
using LiverpoolWebsite.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiverpoolWebsite.DAL.Repositories
{
    public class MatchRepository : IMatchRepository
    {
        private readonly AppDbContext _context;

        public MatchRepository(AppDbContext context)
        {
            _context = context;
        }
        // functia creeaza un meci cu informatiile primite din front-end
        public async Task Create(Match matchModel)
        {
            Match match = new Match
            {
                TeamId = matchModel.TeamId,
                HomeOrAway = matchModel.HomeOrAway,
                GoalsFor = matchModel.GoalsFor,
                GoalsAgainst = matchModel.GoalsAgainst,
                CompetitionId = matchModel.CompetitionId,
                MatchDate = matchModel.MatchDate
            };

            await _context.Matches.AddAsync(match);
            await _context.SaveChangesAsync();
        }

        // functia sterge un meci cu id-ul primit din front-end
        public async Task<bool> Delete(int match_id)
        {
            var match = await _context.Matches.FindAsync(match_id);
            if (match != null)
            {
                _context.Matches.Remove(match);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        // functie care ia din baza de date toate meciurile care s-au jucat intr-o competitie
        public async Task<List<Match>> GetMatchesByComp(string comp)
        {
            var matches = await _context.Matches.Include(x => x.Team).ThenInclude(x => x.Stadium).Include(x => x.Competition).Where(x => x.Competition.Name == comp && x.MatchDate < DateTime.Now).OrderBy(x => x.MatchDate).ToListAsync();
            return matches;
        }

        // functie care modfica un meci cu datele primite din front-end
        // tot printr-un form si verific ce campuri au fost completate in form
        // pentru a le modifica)
        public async Task<bool> Update(Match match)
        {
            var matchUp = await _context.Matches.FindAsync(match.MatchId);

            if (matchUp != null)
            {
                var defMatch = new Match();
                System.Reflection.PropertyInfo[] properties = typeof(Match).GetProperties();
                foreach (var prop in properties)
                {
                    var value = prop.GetValue(match);
                    var valDef = prop.GetValue(defMatch);
                    if (value != null)
                    {
                        if (valDef == null || value.ToString() != valDef.ToString())
                        {
                            prop.SetValue(matchUp, value);
                        }
                    }
                }

                _context.Matches.Update(matchUp);
                await _context.SaveChangesAsync();

                return true;
            }
            return false;
        }

        // functie ce returneaza meciurile ce inca nu s-au jucat
        public async Task<List<Match>> GetFutureMatches()
        {
            var matches = await _context.Matches.Include(x => x.Team).ThenInclude(x => x.Stadium).Include(x => x.Competition).Where(x => x.MatchDate > DateTime.Now).OrderBy(x => x.MatchDate).ToListAsync();
            return matches;
        }
    }
}

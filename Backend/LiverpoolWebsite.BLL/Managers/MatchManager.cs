using LiverpoolWebsite.BLL.Interfaces;
using LiverpoolWebsite.DAL.Entities;
using LiverpoolWebsite.DAL.Interfaces;
using LiverpoolWebsite.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiverpoolWebsite.BLL.Managers
{
    public class MatchManager : IMatchManager
    {
        private readonly IMatchRepository _matchRepo;

        public MatchManager(IMatchRepository matchRepo)
        {
            _matchRepo = matchRepo;
        }

        // manager pentru a lua toate meciurile care s-au disputat la o anumita
        // competite(in cazul in care comp != "fixtures"), sau toate meciurile
        // care inca nu s-au disputat
        public async Task<List<MatchModel>> GetMatches(string comp)
        {
            if (comp == "fixtures")
            {
                var matches = await _matchRepo.GetFutureMatches();
                return convertToList(matches);
            }
            else
            {
                var matches = await _matchRepo.GetMatchesByComp(comp);
                return convertToList(matches);
            }
        }

        // se creaza un meci
        public async Task CreateMatch(Match match)
        {
            await _matchRepo.Create(match);
        }

        // se da update la meci (data, adversar, etc)
        public async Task<bool> UpdateMatch(Match match)
        {
           bool res = await _matchRepo.Update(match);
           return res;
        }

        // sterge un meci
        public async Task<bool> DeleteMatch(int match_id)
        {
            bool res = await _matchRepo.Delete(match_id);
            return res;
        }

        // functie privata care transforma o lista de Match intr-o lista
        // de MatchModel
        private List<MatchModel> convertToList(List<Match> matches)
        {
            var list = new List<MatchModel>();

            foreach (var match in matches)
            {
                var matchModel = new MatchModel
                {
                    HomeOrAway = match.HomeOrAway,
                    Opponent = match.Team.Name,
                    Stadium = (match.HomeOrAway ? "Anfield" : match.Team.Stadium.Name),
                    GoalsFor = match.GoalsFor,
                    GoalsAgainst = match.GoalsAgainst,
                    Competition = match.Competition.Name,
                    MatchDate = match.MatchDate,
                    PhotoUrl = match.Team.photoUrl
                };

                list.Add(matchModel);
            }

            return list;
        }
    }
}

using LiverpoolWebsite.BLL.Interfaces;
using LiverpoolWebsite.DAL.Entities;
using LiverpoolWebsite.DAL.Interfaces;
using LiverpoolWebsite.DAL.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiverpoolWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchController : ControllerBase
    {
        private readonly IMatchManager _matchManager;

        public MatchController(IMatchManager matchManager)
        {
            _matchManager = matchManager;
        }

        // ednpoint pentru a returna toate meciurile dintr-o competitie (UCL, PL, EFL, etc)
        [HttpGet("{comp}")]
        [Authorize]
        public async Task<IActionResult> GetCompMatches(string comp)
        {
            var list = await _matchManager.GetMatches(comp);
            return Ok(list);
        }

        // endpoint pentru a modifica un meci
        [HttpPut("update_match")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateMatch([FromBody] Match match)
        {
            bool res = await _matchManager.UpdateMatch(match);
            return Ok(res);
        }

        // endpoint pentru a adauga un meci
        [HttpPost("add_match")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddMatch([FromBody] Match match)
        {
            await _matchManager.CreateMatch(match);
            return Ok();
        }

        // endpoint pentru a sterge un meci
        [HttpDelete("delete_match/{match_id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteMatch(int match_id)
        {
            bool res = await _matchManager.DeleteMatch(match_id);
            return Ok(res);
        }
    }   
}

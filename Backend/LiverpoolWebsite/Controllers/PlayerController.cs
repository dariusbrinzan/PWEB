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
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerManager _playerManager;

        public PlayerController(IPlayerManager playerManager)
        {
            _playerManager = playerManager;
        }

        // endpoint pentru a returna toti jucatorii de pe o anumita pozitie
        [HttpGet("{pos}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetByPos(string pos)
        {
            var list = await _playerManager.GetPlayers(pos);
            return Ok(list);
        }

        // endpoint pentru a modifica un jucator
        [HttpPut("update_player")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdatePlayer([FromBody] Player player)
        {
            bool res = await _playerManager.UpdatePlayer(player);
            return res ? Ok(res) : BadRequest("Failed to update player.");
        }

        // endpoint pentru a adauga un jucator
        [HttpPost("add_player")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddPlayer([FromBody] Player player)
        {
            await _playerManager.CreatePlayer(player);
            return Ok();
        }

        // endpoint pentru a sterge un jucator
        [HttpDelete("delete_player/{player_id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletePlayer(int player_id)
        {
            bool res = await _playerManager.DeletePlayer(player_id);
            return res ? Ok(res) : BadRequest("Failed to delete player.");
        }
    }
}

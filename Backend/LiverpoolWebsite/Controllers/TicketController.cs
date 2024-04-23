using LiverpoolWebsite.BLL.Interfaces;
using LiverpoolWebsite.DAL.Interfaces;
using LiverpoolWebsite.DAL.Models;
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
    public class TicketController : ControllerBase
    {
        private readonly ITicketManager _ticketManager;
        public TicketController(ITicketManager ticketManager)
        {
            _ticketManager = ticketManager;
        }

        // endpoint pentru a returna biletele tuturor
        // meciurilor nedisputate inca (game_date > current_date)
        [HttpGet("get_all_tickets")]
        public async Task<IActionResult> GetMatchTickets()
        {
            List<FixtureModel> list = await _ticketManager.GetTickets();
            return Ok(list);
        }

        // endpoint pentru modificarea biletelor
        [HttpPut("update_tickets/{match_id}/{oldStand}/{newStand}/{nr}")]
        public async Task<IActionResult> UpdateTickets(int match_id, string oldStand, string newStand, int nr)
        {
            int res = await _ticketManager.UpdateTickets(match_id, oldStand, newStand, nr);
            return Ok(res);
        }

        // endpoint pentru crearea biletelor
        [HttpPost("add_tickets/{match_id}/{stand}/{nr}")]
        public async Task<IActionResult> AddTickets(int match_id, string stand, int nr)
        {
            await _ticketManager.CreateTickets(match_id, stand, nr);
            return Ok();
        }

        // endpoint pentru stergerea biletelor
        [HttpDelete("delete_tickets/{match_id}/{stand}/{nr}")]
        public async Task<IActionResult> DeleteTickets(int match_id, string stand, int nr)
        {
            int res = await _ticketManager.DeleteTickets(match_id, stand, nr);
            return Ok(res);
        }
    }
}

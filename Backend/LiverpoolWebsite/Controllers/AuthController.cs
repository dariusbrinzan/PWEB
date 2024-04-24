using LiverpoolWebsite.BLL.Interfaces;
using LiverpoolWebsite.BLL.DTOs;
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
    public class AuthController : ControllerBase
    {
        private readonly IAuthManager _authManager;

        public AuthController(IAuthManager authManager)
        {
            _authManager = authManager;
        }

        // endpoint pentru inregistrare
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            var result = await _authManager.Register(registerDTO);
            return result ? Ok(result) : BadRequest(result);
        }

        // endpoint pentru logare
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var result = await _authManager.Login(loginDTO);
            return result.Success ? Ok(result) : BadRequest("Failed to login");
        }

        // endpoint pentru refresh token
        [HttpPost("Refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshDTO refreshDTO)
        {
            var result = await _authManager.Refresh(refreshDTO);
            return !result.Contains("Bad") ? Ok(result) : BadRequest("Failed to refresh");
        }
    }
}

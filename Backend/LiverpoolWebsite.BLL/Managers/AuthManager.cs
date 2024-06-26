﻿using LiverpoolWebsite.BLL.Interfaces;
using LiverpoolWebsite.BLL.DTOs;
using LiverpoolWebsite.DAL;
using LiverpoolWebsite.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiverpoolWebsite.BLL.Managers
{
    public class AuthManager : IAuthManager
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenHelper _tokenHelper;
        private readonly AppDbContext _context;

        public AuthManager(UserManager<User> userManager,
            SignInManager<User> signInManager,
            ITokenHelper tokenHelper,
            AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHelper = tokenHelper;
            _context = context;
        }

        // manager pentru login, returneaza daca login-ul s-a realizat cu succes sau nu
        public async Task<LoginResult> Login(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (user == null)
                return new LoginResult
                {
                    Success = false
                };
            else
            {
                var result = await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, false);
                if (result.Succeeded)
                {
                    var token = await _tokenHelper.CreateAccessToken(user);
                    var refreshToken = _tokenHelper.CreateRefreshToken();

                    user.RefreshToken = refreshToken;
                    await _userManager.UpdateAsync(user);

                    return new LoginResult
                    {
                        Success = true,
                        AccessToken = token,
                        RefreshToken = refreshToken
                    };
                }
                else
                    return new LoginResult
                    {
                        Success = false
                    };
            }

        }

        // manager pentru inregistrare, returneaza daca aceasta s-a realizat cu succes sau nu
        public async Task<bool> Register(RegisterDTO registerDTO)
        {
            var count = await _context.Users.FirstOrDefaultAsync();
            // primul user introdus in baza de date primeste rolul de Admin
            if (count != null)
                registerDTO.Role = "User";
            else
                registerDTO.Role = "Admin";

            var user = new User
            {
                Email = registerDTO.Email,
                UserName = registerDTO.Email
            };

            var result = await _userManager.CreateAsync(user, registerDTO.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, registerDTO.Role);
                return true;  
            }
            else
            {
                return false;
            }
        }

        // manager pentru refresh token
        public async Task<string> Refresh(RefreshDTO refreshDTO)
        {
            var principal = _tokenHelper.GetPrincipalFromExpiredToken(refreshDTO.AccessToken);
            var username = principal.Identity.Name;

            var user = await _userManager.FindByEmailAsync(username);

            if (user.RefreshToken != refreshDTO.RefreshToken)
                return "Bad Refresh";

            var newJwtToken = await _tokenHelper.CreateAccessToken(user);

            return newJwtToken;
        }
    }
}


using LiverpoolWebsite.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiverpoolWebsite.BLL.Interfaces
{
    public interface IAuthManager
    {
        Task<bool> Register(RegisterDTO registerModel);
        Task<LoginResult> Login(LoginDTO loginModel);
        Task<string> Refresh(RefreshDTO refreshModel);
    }
}

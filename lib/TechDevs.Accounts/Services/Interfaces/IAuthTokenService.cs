﻿using System.Threading.Tasks;
using TechDevs.Shared.Models;

namespace TechDevs.Accounts
{
    public interface IAuthTokenService<TAuthUser> where TAuthUser : AuthUser, new()
    {
        string CreateToken(string userId);
    }
}
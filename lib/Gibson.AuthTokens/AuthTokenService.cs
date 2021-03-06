﻿using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Gibson.AuthTokens
{
    public class AuthTokenService : IAuthTokenService
    {
        private readonly string _tokenSecret;

        public AuthTokenService()
        {
            _tokenSecret = "TechDevsKeyTechDevsKeyTechDevsKeyTechDevsKeyTechDevsKeyTechDevsKeyTechDevsKeyTechDevsKeyTechDevsKeyTechDevsKeyTechDevsKeyTechDevsKeyTechDevsKeyTechDevsKey";
        }

        public AuthTokenService(string secret)
        {
            _tokenSecret = secret;
        }

        public string CreateToken(Guid userId, string clientKey, Guid clientId)
        {
            if (userId == Guid.Empty) throw new ArgumentException("UserId was an empty Guid");
            if (clientId == Guid.Empty) throw new ArgumentException("ClientId was an empty Guid");
            if (string.IsNullOrEmpty(clientKey)) throw new ArgumentException("UserId was an empty Guid");
            // Authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_tokenSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userId.ToString()),
                    new Claim("Gibson-ClientKey", clientKey),
                    new Claim("Gibson-ClientId", clientId.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var result = tokenHandler.WriteToken(token);
            return result;
        }
    }
}

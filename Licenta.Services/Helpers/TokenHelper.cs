﻿using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Licenta.Core.Entities;
using Licenta.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Licenta.Core.Interfaces;

namespace Licenta.Services.Helpers;

public class TokenHelper : ITokenHelper
{

    private readonly UserManager<User> _userManager;
    private readonly IRepository<UserRole> _userRoleRepository;

    public TokenHelper(UserManager<User> userManager, IRepository<UserRole> userRoleRepository)
    {
        _userManager = userManager;
        _userRoleRepository = userRoleRepository;
    }

    public async Task<string> CreateAccessToken(User _User)
    {
        var userId = _User.Id.ToString();
        var email = _User.Email;
        var userName = _User.UserName;

        var roles = await _userManager.GetRolesAsync(_User);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userId),
            new Claim(ClaimTypes.Name, userName),
            new Claim(ClaimTypes.Email, email),
        };

        var userRole = _userRoleRepository
            .AsQueryable()
            .Where(x => x.UserId == _User.Id && x.RoleId == 2)
            .FirstOrDefault();

        if (userRole != null) 
        {
            claims.Add(new Claim("partnerId", userRole.PartnerId.ToString()));
        }

        foreach (var role in roles) claims.Add(new Claim(ClaimTypes.Role, role));

        var secret = "123456789012345689999";

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddMinutes(800),
            SigningCredentials = creds
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    public string CreateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string _Token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            RequireExpirationTime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("123456789012345689999")),
            ValidateIssuer = false,
            ValidateAudience = false
        };
        IdentityModelEventSource.ShowPII = true;
        var tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken securityToken;

        var principal = tokenHandler.ValidateToken(_Token, tokenValidationParameters, out securityToken);
        var jwtSecurityToken = securityToken as JwtSecurityToken;
        if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals("hs512", StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");

        return principal;
    }
}


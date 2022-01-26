using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ChalkCode.Auth;
using Core.Users;
using Database;
using JWTWebAuthentication.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ChalkCode.Auth
{
    public class JWTManagerRepository : IJWTManagerRepository
    {
		private readonly IConfiguration iconfiguration;
		public JWTManagerRepository(IConfiguration iconfiguration)
		{
			this.iconfiguration = iconfiguration;
		}
		public async Task<Tokens> Authenticate(User users)
        {
			

			var tokenHandler = new JwtSecurityTokenHandler();
			var tokenKey = Encoding.UTF8.GetBytes(iconfiguration["JWT:Key"]);
			var claims = new List<Claim>();
			claims.Add(new Claim(ClaimTypes.Name, users.name));
			claims.Add(new Claim(ClaimTypes.NameIdentifier, users.name));
			claims.Add(new Claim(ClaimTypes.Role, users.Role));
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.UtcNow.AddMinutes(10),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			return await Task<Tokens>.Run(()=> new Tokens { Token = tokenHandler.WriteToken(token) });
		}
    }
}

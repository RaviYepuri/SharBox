using DataRooms.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataRooms.WEBAPI.Helpers
{
    public class JWTAuth
    {
        public AuthenticateResponse GetResponse(User user,IConfiguration _configuration)
        {
            if (user != null && user.Id > 0)
            {
                var authSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                var tokenDescriptor = new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                    Audience = _configuration["JWT:ValidAudience"],
                    Issuer = _configuration["JWT:ValidIssuer"],
                    Expires = DateTime.Now.AddHours(5),
                    SigningCredentials = new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return new AuthenticateResponse(user, tokenHandler.WriteToken(token),tokenDescriptor.Expires);
            }
            return null;
        }
    }
}

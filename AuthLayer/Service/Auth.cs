using System;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using AuthLayer.Repository;

namespace AuthLayer.Service
{
    public class Auth:IAuth
    {
        private IConfiguration _configuration = null;
        public Auth(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateToken(string username,string password, int? userid)
        {
            string key = _configuration.GetSection("jwtkey").Value.ToString();
            int hour = Convert.ToInt32(_configuration.GetSection("jwtexpiryhour").Value);

            var tokenhandler = new JwtSecurityTokenHandler();
            var tokenkey = Encoding.ASCII.GetBytes(key);

            var tokenDescriptor = new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Authentication, Convert.ToString(userid)),
                        new Claim(ClaimTypes.UserData, username)
                    }),
                Expires = DateTime.UtcNow.AddHours(hour),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenkey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenhandler.CreateToken(tokenDescriptor);

            return tokenhandler.WriteToken(token);
        }
    }
}
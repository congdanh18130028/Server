using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shop.DataAccess;
using Shop.DTO;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly ShopContext _context;
        private IConfiguration _config;
        public AuthenticateService(ShopContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        public UserDto Authenticate(string email, string password)
        {
            var user = _context.User.Select(u => new UserDto()
            {
                Id = u.Id,
                Name = u.Name,
                Phone = u.Phone,
                Address = u.Address,
                Role = u.Role,
                Email = u.Email,
                Password = u.Password
            }).SingleOrDefault(u => u.Email == email);
            if(user != null)
            {
                if (user.Password.Equals(password))
                {
                    return user;
                }
                return null;
            }
            return user;

        }

        public string GenerateJSONWebToken(UserDto user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials
                );
            var encodetoken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodetoken;
        }
    }
}

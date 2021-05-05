using Shop.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Services
{
    public interface IAuthenticateService
    {
        public UserDto Authenticate(string email, string password);
        public string GenerateJSONWebToken(UserDto customer);
    }
}

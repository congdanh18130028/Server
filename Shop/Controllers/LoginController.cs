using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IAuthenticateServices _authenticateService;
        public LoginController(IAuthenticateServices authenticateService)
        {
            _authenticateService = authenticateService;
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = _authenticateService.Authenticate(email, password);
            IActionResult response = Unauthorized();
            if (user != null)
            {
                var tokenStr = _authenticateService.GenerateJSONWebToken(user);
                response = Ok(new { token = tokenStr });
            }
            return response;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        //public string getEmail()
        //{
        //    var identity = HttpContext.User.Identity as ClaimsIdentity;
        //    IList<Claim> claim = identity.Claims.ToList();
        //    var email = claim[0].Value;
        //    return email;
        //}
        public string get()
        {
            return "huhu";
        }
    }
}

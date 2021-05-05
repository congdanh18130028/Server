using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Shop.DataAccess;
using Shop.DTO;
using Shop.Models;
using Shop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private IUserServices _userServices;
        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            return Ok(_userServices.GetUsers());
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var customer = _userServices.GetUser(id);
            if (customer != null)
            {
                return Ok(customer);
            }
            return NotFound($"Customer with id: {id} was not found");
        }

        [HttpPost]
        public IActionResult AddUser(UserDto user)
        {

            return Ok(_userServices.AddUser(user));
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] JsonPatchDocument<UserDto> patch)
        {
            var user = _userServices.GetUser(id);

            if (user == null)
            {
                return BadRequest();
            }
            patch.ApplyTo(user, ModelState);

            return Ok(_userServices.UpdateUser(user));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var customer = _userServices.GetUser(id);
            if (customer != null)
            {
                return Ok(_userServices.DeleteUser(id));
            }
            return NotFound($"Customer with id: {id} was not found");
        }



    }


}

using Microsoft.AspNetCore.Http;
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
    [ApiController]
   
    public class CustomerController : ControllerBase
    {
        private ICustomerServices _customerServices;
        public CustomerController(ICustomerServices customerServices)
        {
            _customerServices = customerServices;
        }

        [HttpGet]
        [Route("api/[controller]")]
        public IActionResult GetCustomers()
        {
            return Ok(_customerServices.GetCustomers());
        }

        [HttpGet("id")]
        [Route("api/[controller]/{id}")]
        public IActionResult GetCustomer(int id)
        {
            var customer = _customerServices.GetCustomer(id);
            if (customer != null)
            {
                return Ok(customer);
            }
            return NotFound($"Customer with id: {id} was not found");
        }

        [HttpPost]
        [Route("api/[controller]")]
        public IActionResult AddCustomer(CustomerDto customer)
        {

            return Ok(_customerServices.AddCustomer(customer));
        }

        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            var customer = _customerServices.GetCustomer(id);
            if (customer != null)
            {
                return Ok(_customerServices.DeleteCustomer(id));
            }
            return NotFound($"Customer with id: {id} was not found");
        }

    }


}

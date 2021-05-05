using Shop.DataAccess;
using Shop.DTO;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Services
{
    public class CustomerServices : ICustomerServices
    {
        private readonly ShopContext _context;
        public CustomerServices(ShopContext context)
        {
            _context = context;

        }
        public CustomerDto AddCustomer(CustomerDto customer)
        {
            Customer _customer = new Customer();
            _customer.Name = customer.Name;
            _customer.Phone = customer.Phone;
            _customer.Address = customer.Address;
            _customer.Role = "Customer";
            _customer.Email = customer.Email;
            _customer.Password = customer.Password;
    
            _context.Customer.Add(_customer);
            _context.SaveChanges();
            
            return customer;
        }

        public Customer DeleteCustomer(int id)
        {
            var customer = _context.Customer.Where(c => c.Id == id).First<Customer>();
            _context.Customer.Remove(customer) ;
            _context.SaveChanges();
            return customer;
        }

        public CustomerDto GetCustomer(int id)
        {
            var customer = _context.Customer.Select(c => new CustomerDto()
            {
                Id = c.Id,
                Name = c.Name,
                Phone = c.Phone,
                Address = c.Address,
                Role = c.Role,
                Email = c.Email,
                Password = c.Password
            }
            ).SingleOrDefault(c => c.Id == id);

            return customer;
        }

        public List<CustomerDto> GetCustomers()
        {
            var customers = from c in _context.Customer
                            select new CustomerDto()
                            {
                                Id = c.Id,
                                Name = c.Name,
                                Phone = c.Phone,
                                Address = c.Address,
                                Role = c.Role,
                                Email = c.Email,
                                Password = c.Password
                            };
            return customers.ToList<CustomerDto>();
        }

        public Customer UpdateCustomer(int id, Customer customer)
        {
            throw new NotImplementedException();
        }

    }
}

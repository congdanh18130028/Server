using Shop.DTO;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Services

{
    public interface ICustomerServices
    {
        public List<CustomerDto> GetCustomers();
        public CustomerDto GetCustomer(int id);
        public CustomerDto AddCustomer(CustomerDto customer);
        public Customer UpdateCustomer(int id, Customer customer);
        public Customer DeleteCustomer(int id);

    }
}

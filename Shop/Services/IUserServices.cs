using Microsoft.AspNetCore.JsonPatch;
using Shop.DTO;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Services

{
    public interface IUserServices
    {
        public List<UserDto> GetUsers();
        public UserDto GetUser(int id);
        public UserDto AddUser(UserDto customer);
        public User DeleteUser(int id);
        public void SaveChanges();
        public UserDto UpdateUser(UserDto user);
    }
}

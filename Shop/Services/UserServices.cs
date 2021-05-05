using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Shop.DataAccess;
using Shop.DTO;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace Shop.Services
{
    public class UserServices : IUserServices
    {
        private readonly ShopContext _context;
        public UserServices(ShopContext context)
        {
            _context = context;

        }
        public UserDto AddUser(UserDto user)
        {
            User _user = new User();
            _user.Name = user.Name;
            _user.Phone = user.Phone;
            _user.Address = user.Address;
            _user.Role = "Customer";
            _user.Email = user.Email;
            _user.Password = user.Password;
    
            _context.User.Add(_user);
            _context.SaveChanges();
            
            return user;
        }

        private User ConvertDtoToEntity(UserDto user)
        {
            User _user = new User();
            _user.Id = user.Id;
            _user.Name = user.Name;
            _user.Phone = user.Phone;
            _user.Address = user.Address;
            _user.Role = user.Role;
            _user.Email = user.Email;
            _user.Password = user.Password;
            return _user;

        }

        public User DeleteUser(int id)
        {
            var user = _context.User.Where(u => u.Id == id).First<User>();
            _context.User.Remove(user) ;
            _context.SaveChanges();
            return user;
        }

        public UserDto GetUser(int id)
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
            }
            ).SingleOrDefault(u => u.Id == id);

            return user;
        }

        public List<UserDto> GetUsers()
        {
            var users = from u in _context.User
                            select new UserDto()
                            {
                                Id = u.Id,
                                Name = u.Name,
                                Phone = u.Phone,
                                Address = u.Address,
                                Role = u.Role,
                                Email = u.Email,
                                Password = u.Password
                            };
            return users.ToList<UserDto>();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public UserDto UpdateUser(UserDto user)
        {
            User _user = ConvertDtoToEntity(user);
            _context.Entry(_user).State = EntityState.Modified;
            _context.SaveChanges();
            return (GetUser(user.Id));
        }
    }
}

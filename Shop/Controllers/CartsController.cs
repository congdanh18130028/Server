using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Dtos;
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
    public class CartsController : ControllerBase
    {
        private readonly ICartServices _cartServices;
        private readonly IMapper _mapper;
        public CartsController(ICartServices cartServices, IMapper mapper)
        {
            _cartServices = cartServices;
            _mapper = mapper;
        }

        [HttpGet("{cartId}")]
        public ActionResult<IEnumerable<CartItemReadDto>> GetCartItems(int cartId)
        {
            var cartItems = _cartServices.GetItems(cartId);
            return Ok(_mapper.Map<IEnumerable<CartItemReadDto>>(cartItems));
        }

        [HttpGet("{id}, {cartId}", Name = "GetCartItem")]
        public ActionResult GetCartItem(int id, int cartId)
        {
            var cartItem = _cartServices.GetItem(id, cartId);
            return Ok(_mapper.Map<CartItemReadDto>(cartItem));
        }

        [HttpPost]
        public ActionResult AddCartItem(CartItemCreateDto cartItem)
        {
            var _cartItem = _mapper.Map<CartItem>(cartItem);
            _cartServices.AddItem(_cartItem);
            _cartServices.SaveChanges();
            var cartItemReadDto = _mapper.Map<CartItemReadDto>(_cartItem);
            return CreatedAtRoute(nameof(GetCartItem), new { id = cartItemReadDto.Id, cartId = cartItemReadDto.CartId }, cartItemReadDto);

        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCartItem(int id)
        {
            _cartServices.RemoveItem(id);
            _cartServices.SaveChanges();
            return NoContent();
        }
       
        [HttpPatch("increase/{id}")]
        public ActionResult IncreaseQuantityItem(int id)
        {
            _cartServices.IncreaseItem(id);
            _cartServices.SaveChanges();
            return NoContent();
        }


        [HttpPatch("decrease/{id}")]
        public ActionResult DecreaseQuantityItem(int id)
        {
            _cartServices.DecreaseItem(id);
            _cartServices.SaveChanges();
            return NoContent();
        }

    }
}

using Shop.DataAccess;
using Shop.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Services
{
    public class CartServices : ICartServices
    {
        private readonly ShopContext _context;
        public CartServices(ShopContext context)
        {
            _context = context;
        }
        public void AddItem(CartItem item)
        {
            if(item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            _context.CartItems.Add(item);
            
        }

        public void CreateCart(int UId)
        {
            if(UId == 0)
            {
                throw new ArgumentNullException(nameof(UId));
            }
            Cart c = new Cart();
            c.UserID = UId;
            _context.Carts.Add(c);

        }

        public void DecreaseItem(int itemId)
        {
            if (itemId == 0)
            {
                throw new ArgumentNullException(nameof(itemId));
            }
            var _cartItem = _context.CartItems.FirstOrDefault(i => i.Id == itemId);
            if (_cartItem == null)
            {
                throw new ArgumentNullException(nameof(_cartItem));
            }
            _cartItem.Quantity -= 1;
            _context.CartItems.Update(_cartItem);
        }

        public void DropCart(int UId)
        {
            if(UId == 0)
            {
                throw new ArgumentNullException(nameof(UId));
            }
            var _cart = _context.Carts.FirstOrDefault(c => c.UserID == UId);
            if(_cart == null)
            {
                throw new ArgumentNullException(nameof(UId));
            }
            if(_cart.CartItems != null)
            {
                _context.CartItems.RemoveRange(_cart.CartItems);

            }
            _context.Carts.Remove(_cart);
        }

        public CartItem GetItem(int id, int cartId)
        {
            return _context.CartItems.Where(i => i.CartId == cartId && i.Id == id)
                                     .FirstOrDefault();
        }

        public List<CartItem> GetItems(int cartId)
        {
            return _context.CartItems.Where(i => i.CartId == cartId).ToList();
            
        }

        public void IncreaseItem(int itemId)
        {
            if(itemId == 0)
            {
                throw new ArgumentNullException(nameof(itemId));
            }
            var _cartItem = _context.CartItems.FirstOrDefault(i => i.Id == itemId);
            if(_cartItem == null)
            {
                throw new ArgumentNullException(nameof(_cartItem));
            }
            _cartItem.Quantity += 1;
            _context.CartItems.Update(_cartItem);
                     
        }

        public void RemoveItem(int itemId)
        {
            if (itemId ==0)
            {
                throw new ArgumentNullException(nameof(itemId));
            }
            var _cartItem = _context.CartItems.FirstOrDefault(i => i.Id == itemId);
            if (_cartItem == null)
            {
                throw new ArgumentNullException(nameof(_cartItem));
            }
            _context.CartItems.Remove(_cartItem);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}

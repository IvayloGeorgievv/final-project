using FinalProject.DataBase;
using FinalProject.Domain.Models;
using FinalProject.Domain.ViewModels.Order;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Repositories
{
    public class CartRepository
    {
        private readonly ApplicationDbContext _context;

        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Cart> GetCartItemById(int id)
        {
            return await _context.Cart.FindAsync(id);
        }

        public async Task<IEnumerable<Cart>> GetCartByUserId(int userId)
        {
            return await _context.Cart
                .Where(c => c.UserId == userId)
                .ToListAsync();
        }

        public async Task<Cart> AddToCart(Cart cart)
        {
            await _context.Cart.AddAsync(cart);
            await _context.SaveChangesAsync();

            return cart;
        }

        public async Task<Cart> UpdateCartProduct(Cart cart)
        {
            var existingCart = await _context.Cart.FirstOrDefaultAsync(c => c.Id == cart.Id);

            if(existingCart != null)
            {
                existingCart.Quantity = cart.Quantity;
                existingCart.UnitPrice = cart.UnitPrice;

                await _context.SaveChangesAsync();

                return existingCart;
            }
            return null;
        }

        public async Task<bool> RemoveFromCart(int id)
        {
            var cart = await _context.Cart.FindAsync(id);

            if (cart == null)
            {
                return false;
            }

            _context.Cart.Remove(cart);
            await _context.SaveChangesAsync();

            return true;
        }
        
    }
}

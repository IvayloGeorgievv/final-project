using FinalProject.Domain.Models;
using FinalProject.Domain.ViewModels.Cart;
using FinalProject.Repositories;

namespace FinalProject.Services
{
    public class CartService
    {
        private readonly CartRepository _cartRepository;
        private readonly IConfiguration _configuration;

        public CartService(CartRepository cartRepository, IConfiguration configuration)
        {
            _cartRepository = cartRepository;
            _configuration = configuration;
        }

        public async Task<IEnumerable<CartVM>> GetCartByUserId(int userId)
        {
            var cart = await _cartRepository.GetCartByUserId(userId);

            return cart.Select(cart => new CartVM
            {
                Id = cart.Id,
                UserId = userId,
                MobilePhoneId = cart.MobilePhoneId,
                Quantity = cart.Quantity,
                UnitPrice = cart.UnitPrice
            });
        }

        public async Task<CartVM> AddCartItem(CreateCartVM createCartVM)
        {
            var cart = new Cart
            {
                UserId = createCartVM.UserId,
                MobilePhoneId = createCartVM.MobilePhoneId,
                Quantity = createCartVM.Quantity,
                UnitPrice = createCartVM.UnitPrice
            };

            var createCart = await _cartRepository.AddToCart(cart);

            return new CartVM
            {
                Id = createCart.Id,
                UserId = createCart.UserId,
                MobilePhoneId = createCart.MobilePhoneId,
                Quantity = createCart.Quantity,
                UnitPrice = createCart.UnitPrice
            };
        }

        public async Task<CartVM> UpdateCartItem(int id,UpdateCartVM updateCartVM)
        {
            var existingCart = await _cartRepository.GetCartItemById(id);

            if(existingCart != null)
            {
                return null;
            }

            existingCart.Quantity = updateCartVM.Quantity;

            return new CartVM
            {
                Id = existingCart.Id,
                UserId = existingCart.UserId,
                MobilePhoneId = existingCart.MobilePhoneId,
                Quantity = updateCartVM.Quantity,
                UnitPrice = existingCart.UnitPrice
            };
        }

        public async Task<bool> DeleteCartItem(int id)
        {
            return await _cartRepository.RemoveFromCart(id);
        }
    }
}

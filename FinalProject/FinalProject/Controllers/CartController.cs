using FinalProject.Domain.ViewModels.Cart;
using FinalProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : Controller
    {
        private readonly CartService _cartService;

        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetCartByUserId(int userId)
        {
            try
            {
                var cartItems = _cartService.GetCartByUserId(userId);

                return Ok(cartItems);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddCartItem([FromBody] CreateCartVM createCartVM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return null;
                }

                var cartItem = await _cartService.AddCartItem(createCartVM);
                return Ok(cartItem);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error: " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCartItem(int id, [FromBody] UpdateCartVM updateCartVM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return null;
                }

                var updatedCartItem = _cartService.UpdateCartItem(id, updateCartVM);
                if(updatedCartItem == null)
                {
                    return NotFound(new { Message = $"Cart item with ID {id} not found." });
                }

                return Ok(updatedCartItem);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCartItem(int id)
        {
            try
            {
                var result = await _cartService.DeleteCartItem(id);

                if (!result)
                {
                    return NotFound(new { Message = $"Cart item with ID {id} not found." });
                }

                return NoContent();
            }
            catch(Exception ex)
            {
                return StatusCode(500, "Error: " + ex.Message);
            }
        }
    }
}

using FinalProject.Domain.ViewModels.Order;
using FinalProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            try
            {
                var orders = await _orderService.GetAllOrders();

                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error: " + ex.Message);
            }
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetOrdersForUser(int userId)
        {
            try
            {
                var orders = await _orderService.GetOrdersForUser(userId);

                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new {Message = "Error: " + ex.Message});
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder([FromBody] CreateOrderVM orderVM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var order = await _orderService.AddOrder(orderVM);

                return Ok(order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error: " + ex.Message });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveOrder(int orderId)
        {
            try
            {
                var result = await _orderService.RemoveOrder(orderId);

                if (!result)
                {
                    return NotFound(new { Message = $"Order with ID {orderId} not found." });
                }

                return NoContent();
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { Message = "Error: " + ex.Message });
            }
        }
    }
}

using FinalProject.Dtos.Order;
using FinalProject.Models;
using FinalProject.Repositories;

namespace FinalProject.Services
{
    public class OrderService
    {
        private readonly OrderRepository _orderRepository;

        public OrderService(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<OrderDTO>> GetAllOrders()
        {
            var orders = await _orderRepository.GetAllOrders();

            return orders.Select(order => new OrderDTO
            {
                Id = order.Id,
                UserId = order.UserId,
                TotalAmount = order.TotalAmount,
                OrderDate = order.OrderDate,
                Address = order.Address
            });
        }

        public async Task<IEnumerable<OrderDTO>> GetOrdersForUser(int userId)
        {
            var orders = await _orderRepository.GetOrdersForUser(userId);

            return orders.Select(order => new OrderDTO
            {
                Id = order.Id,
                UserId = order.UserId,
                TotalAmount = order.TotalAmount,
                OrderDate = order.OrderDate,
                Address = order.Address
            });
        }

        public async Task<OrderDTO> AddOrder(CreateOrderDTO createOrderDTO)
        {
            var order = new Order
            {
                UserId = createOrderDTO.UserId,
                TotalAmount = createOrderDTO.TotalAmount,
                OrderDate = createOrderDTO.OrderDate,
                Address = createOrderDTO.Address
            };

            var createOrder = await _orderRepository.AddOrder(order);

            return new OrderDTO
            {
                Id = createOrder.Id,
                UserId = createOrder.UserId,
                TotalAmount = createOrder.TotalAmount,
                OrderDate = createOrder.OrderDate,
                Address = createOrder.Address
            };
        }

        public async Task<bool> RemoveOrder(int orderId)
        {
            return await _orderRepository.RemoveOrder(orderId);
        }
    }
}

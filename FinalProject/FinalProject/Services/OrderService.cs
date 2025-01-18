using FinalProject.Domain.Models;
using FinalProject.Domain.ViewModels.Order;
using FinalProject.Domain.ViewModels.OrderProduct;
using FinalProject.Repositories;

namespace FinalProject.Services
{
    public class OrderService
    {
        private readonly OrderRepository _orderRepository;
        private readonly IConfiguration _configuration;

        public OrderService(OrderRepository orderRepository, IConfiguration configuration)
        {
            _orderRepository = orderRepository;
            _configuration = configuration;
        }

        public async Task<IEnumerable<OrderVM>> GetAllOrders()
        {
            var orders = await _orderRepository.GetAllOrders();

            return orders.Select(order => new OrderVM
            {
                Id = order.Id,
                UserId = order.UserId,
                TotalAmount = order.TotalAmount,
                OrderDate = order.OrderDate,
                Address = order.Address
            });
        }

        public async Task<IEnumerable<OrderVM>> GetOrdersForUser(int userId)
        {
            var orders = await _orderRepository.GetOrdersForUser(userId);

            return orders.Select(order => new OrderVM
            {
                Id = order.Id,
                UserId = order.UserId,
                TotalAmount = order.TotalAmount,
                OrderDate = order.OrderDate,
                Address = order.Address
            });
        }

        public async Task<OrderVM> AddOrder(CreateOrderVM createOrderVM)
        {
            var order = new Order
            {
                UserId = createOrderVM.UserId,
                TotalAmount = createOrderVM.TotalAmount,
                OrderDate = createOrderVM.OrderDate,
                Address = createOrderVM.Address
            };

            var createOrder = await _orderRepository.AddOrder(order);

            if(createOrderVM.Products != null && createOrderVM.Products.Any())
            {
                foreach(var product in createOrderVM.Products)
                {
                    var orderProduct = new OrderProduct
                    {
                        OrderId = createOrder.Id,
                        MobilePhoneId = product.MobilePhoneId,
                        Quantity = product.Quantity,
                        UnitPrice = product.UnitPrice
                    };

                    await _orderRepository.AddOrderProduct(orderProduct);
                }
            }

            return new OrderVM
            {
                Id = createOrder.Id,
                UserId = createOrder.UserId,
                TotalAmount = createOrder.TotalAmount,
                OrderDate = createOrder.OrderDate,
                Address = createOrder.Address,
                Products = createOrderVM.Products.Select(p => new OrderProductVM
                {
                    MobilePhoneId = p.MobilePhoneId,
                    Quantity = p.Quantity,
                    UnitPrice = p.UnitPrice
                }).ToList()
            };
        }

        public async Task<bool> RemoveOrder(int orderId)
        {
            await _orderRepository.DeleteOrderProducts(orderId);

            return await _orderRepository.RemoveOrder(orderId);
        }


        /*
         *      OrderProducts methods
         */

        public async Task<IEnumerable<OrderProductVM>> GetProductsByOrderId(int orderId)
        {
            var products = await _orderRepository.GetOrdersProducts(orderId);

            return products.Select(product => new OrderProductVM
            {
                Id = product.Id,
                OrderId = product.OrderId,
                MobilePhoneId = product.MobilePhoneId,
                Quantity = product.Quantity,
                UnitPrice = product.UnitPrice
            });
        }

        public async Task<decimal> GetTotalPriceByOrderId(int orderId)
        {
            return await _orderRepository.GetTotalPriceByOrderId(orderId);
        }

        public async Task<OrderProductVM> AddOrderProduct(CreateOrderProductVM orderProductVM)
        {
            var orderProduct = new OrderProduct
            {
                OrderId = orderProductVM.OrderId,
                MobilePhoneId = orderProductVM.MobilePhoneId,
                Quantity = orderProductVM.Quantity,
                UnitPrice = orderProductVM.UnitPrice
            };

            var addedOrderProduct = await _orderRepository.AddOrderProduct(orderProduct);

            return new OrderProductVM
            {
                Id = addedOrderProduct.Id,
                OrderId = addedOrderProduct.OrderId,
                MobilePhoneId = addedOrderProduct.MobilePhoneId,
                Quantity = addedOrderProduct.Quantity,
                UnitPrice = addedOrderProduct.UnitPrice
            };
        }
    }
}

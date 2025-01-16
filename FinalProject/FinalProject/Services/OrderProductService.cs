using FinalProject.Dtos.OrderProduct;
using FinalProject.Dtos.User;
using FinalProject.Models;
using FinalProject.Repositories;

namespace FinalProject.Services
{
    public class OrderProductService
    {
        private readonly OrderProductRepository _orderProductRepository;

        public OrderProductService(OrderProductRepository orderProductRepository)
        {
            _orderProductRepository = orderProductRepository;
        }

        public async Task<IEnumerable<OrderProductDTO>> GetProductsByOrderId(int orderId)
        {
            var products = await _orderProductRepository.GetOrdersProducts(orderId);

            return products.Select(product => new OrderProductDTO
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
            return await _orderProductRepository.GetTotalPriceByOrderId(orderId);
        }

        public async Task<OrderProductDTO> AddOrderProduct(CreateOrderProductDTO orderProductDTO)
        {
            var orderProduct = new OrderProduct
            {
                OrderId = orderProductDTO.OrderId,
                MobilePhoneId = orderProductDTO.MobilePhoneId,
                Quantity = orderProductDTO.Quantity,
                UnitPrice = orderProductDTO.UnitPrice
            };

            var addedOrderProduct = await _orderProductRepository.AddOrderProduct(orderProduct);

            return new OrderProductDTO
            {
                Id = addedOrderProduct.Id,
                OrderId = addedOrderProduct.OrderId,
                MobilePhoneId = addedOrderProduct.MobilePhoneId,
                Quantity = addedOrderProduct.Quantity,
                UnitPrice = addedOrderProduct.UnitPrice
            };
        }

        public async Task<OrderProductDTO?> UpdateOrderProduct(int id, UpdateOrderProductDTO orderProductDTO)
        {
            var orderProduct = await _orderProductRepository.GetOrderProductById(id);

            if (orderProduct == null)
            {
                throw new Exception($"Order Product with ID {id} not found.");
            }

            orderProduct.Quantity = orderProductDTO.Quantity ?? orderProduct.Quantity;
            orderProduct.UnitPrice = orderProductDTO.UnitPrice ?? orderProduct.UnitPrice;

            await _orderProductRepository.UpdateOrderProduct(orderProduct);

            return new OrderProductDTO
            {
                Id = orderProduct.Id,
                OrderId = orderProduct.OrderId,
                MobilePhoneId = orderProduct.MobilePhoneId,
                Quantity = orderProduct.Quantity,
                UnitPrice = orderProduct.UnitPrice
            };
        }

        public async Task DeleteOrderProducts(int orderId)
        {
            await _orderProductRepository.DeleteOrderProducts(orderId);
        }
    }
}

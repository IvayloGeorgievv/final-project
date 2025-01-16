using FinalProject.Data;
using FinalProject.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Repositories
{
    public class OrderProductRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderProduct>> GetOrdersProducts(int orderId)
        {
            return await _context.OrderProducts
                .Where(o =>  o.OrderId == orderId)
                .ToListAsync();
        }

        public async Task<OrderProduct> GetOrderProductById(int orderId)
        {
            return await _context.OrderProducts.FindAsync(orderId);
        }

        public async Task<decimal> GetTotalPriceByOrderId(int orderId)
        {
            return await _context.OrderProducts
                .Where(o => o.OrderId == orderId)
                .SumAsync(o => o.Quantity * o.UnitPrice);
        }

        public async Task<OrderProduct> AddOrderProduct(OrderProduct orderProducts)
        {
            await _context.OrderProducts.AddAsync(orderProducts);
            await _context.SaveChangesAsync();

            return orderProducts;
        }

        public async Task<OrderProduct> UpdateOrderProduct(OrderProduct orderProduct)
        {
            var existingOrderProduct = await _context.OrderProducts.FindAsync(orderProduct.Id);

            if(existingOrderProduct == null)
            {
                return null;
            }

            existingOrderProduct.Quantity = orderProduct.Quantity;
            existingOrderProduct.UnitPrice = orderProduct.UnitPrice;

            await _context.SaveChangesAsync();

            return existingOrderProduct;
        }

        public async Task DeleteOrderProducts(int orderId)
        {
            var orderProducts = await _context.OrderProducts
                .Where(o => o.OrderId == orderId)
                .ToListAsync();

            if(orderProducts.Any())
            {
                _context.OrderProducts.RemoveRange(orderProducts);

                await _context.SaveChangesAsync();
            }
        }
    }
}

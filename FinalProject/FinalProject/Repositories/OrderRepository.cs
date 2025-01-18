using FinalProject.DataBase;
using FinalProject.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Repositories
{
    public class OrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersForUser(int userId)
        {
            return await _context.Orders
                .Where(o => o.UserId == userId)
                .ToListAsync();
        }

        public async Task<Order> GetOrderById(int id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public async Task<Order> AddOrder(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            return order;
        }

        public async Task<bool> RemoveOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if(order == null)
            {
                return false;
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return true;
        }


        /*
         *          OrderProduct methods
         */

        public async Task<IEnumerable<OrderProduct>> GetOrdersProducts(int orderId)
        {
            return await _context.OrderProducts
                .Where(o => o.OrderId == orderId)
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

        public async Task DeleteOrderProducts(int orderId)
        {
            var orderProducts = await _context.OrderProducts
                .Where(o => o.OrderId == orderId)
                .ToListAsync();

            if (orderProducts.Any())
            {
                _context.OrderProducts.RemoveRange(orderProducts);

                await _context.SaveChangesAsync();
            }
        }
    }
}

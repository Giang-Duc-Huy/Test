using Microsoft.EntityFrameworkCore;

namespace ConsoleApp1
{
    public class OrderService
    {
        private readonly IApplicationDbContext _context;
        
        public OrderService(IApplicationDbContext context)
        {
            _context = context;
        }
        
        public void CreateOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }
        
        public List<Order> GetOrders()
        {
            return _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .ToList();
        }
        
        public void AddProductToOrder(int orderId, Product product, int quantity = 1)
        {
            var order = _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefault(o => o.Id == orderId);
                
            if (order != null)
            {
                order.AddProduct(product, quantity);
                _context.SaveChanges();
            }
        }
    }
}

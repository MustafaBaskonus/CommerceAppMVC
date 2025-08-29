using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(RepositoryContext context) : base(context)
        {
        }

        public IQueryable<Order> Orders => _context.Orders
        .Include(y => y.Lines)
        .ThenInclude(y => y.Product)
        .OrderBy(y => y.Shipped)
        .ThenByDescending(y => y.OrderId);
        public void Complete(int id)
        {
            var order = FindByCondition(y => y.OrderId.Equals(id), true);
            order.Shipped = true;
            
        }

        public Order? GetOneOrder(int id)
        {
           var order= FindByCondition(y => y.OrderId.Equals(id), true);
           if (order == null)
           throw new ArgumentException("Order is not found.");
            return order;
        }

        public int NumberOfInProcess()
        {
            return _context.Orders.Count(y => y.Shipped.Equals(false));
        }

        public void SaveOrder(Order order)
        {
            _context.AttachRange(order.Lines.Select(x => x.Product));
            if (order.OrderId.Equals(0))
                _context.Orders.Add(order);
            _context.SaveChanges();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class OrderManager : IOrderService
    {
        private readonly IRepositoryManager _manager;

        public OrderManager(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public IQueryable<Order> Orders => _manager.Order.Orders;

        public void Complete(int id)
        {
            _manager.Order.Complete(id);
            _manager.Save();
        }

        public Order? GetOneOrder(int id)
        {
          return _manager.Order.GetOneOrder(id);
        }

        public int NumberOfInProcess()
        {
           return _manager.Order.NumberOfInProcess();
        }

        public void SaveOrder(Order order)
        {
           _manager.Order.SaveOrder(order);
        }
    }
}
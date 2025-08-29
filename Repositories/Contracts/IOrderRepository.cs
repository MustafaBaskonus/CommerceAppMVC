using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;

namespace Repositories.Contracts
{
    public interface IOrderRepository: IRepositoryBase<Order>
    {
        IQueryable<Order> Orders { get; }
        Order? GetOneOrder(int id);
        void Complete(int id);
        int NumberOfInProcess();
        void SaveOrder(Order order);
         
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooring.Models.Interfaces
{
    public interface IOrderRepository
    {
        Order LoadOrder(string AccountNumber);
        int GetOrderCount();
        void Remove(string orderNum, DateTime orderDate);
        void Add(Order newOrder);
        void Edit(Order order, string key, DateTime editDate);
        List<Order> List(DateTime order);
    }
}

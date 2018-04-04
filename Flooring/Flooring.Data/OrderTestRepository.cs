using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flooring.Models;
using Flooring.Models.Interfaces;

namespace Flooring.Data
{
    public class OrderTestRepository : IOrderRepository
    {
        private static Dictionary<string, Order> _orderIndex = new Dictionary<string, Order>();
        private static Order _order = new Order
        {
            OrderNumber = 10,
            CustomerName = "Cole Smith",
            State = "IN",
            TaxRate = 6M,
            ProductType = "WOOD",
            Area = 200M,
            CostPerSquareFoot = 5.15M,
            LaborCostPerSquareFoot = 4.75M,
            MaterialCost = (200 * 5.15M),
            LaborCost = (200 * 4.75M),
            Tax = (((200 * 5.15M) + (200 * 4.75M)) * (6 / 100M)),
            Total = ((200 * 5.15M) + (200 * 4.75M) + (((200 * 5.15M) + (200 * 4.75M)) * (6 / 100))),
            OrderDate = DateTime.Today
        };
        public void Edit(Order order, string key, DateTime editDate)
        {
            _orderIndex.Remove(order.OrderNumber.ToString());
            _orderIndex.Add(order.OrderNumber.ToString(), _order);
        }
        public OrderTestRepository()
        {
            _orderIndex.Add("1", _order);
        }
        public int GetOrderCount()
        {
            return _order.OrderNumber + 1;
        }
        public void Remove(string key, DateTime removeDate)
        {
            _orderIndex.Remove("1");
        }
        public void Add (Order order)
        {
            _orderIndex.Add(order.OrderNumber.ToString(), order);
        }
        public void CreateOrderFile(Dictionary<string, Order> order, DateTime fileDate)
        {
            order = _orderIndex;
        }
        public Order LoadOrder(string AccountNumber)
        {
            return _order;
        }
        public string GetFileName(DateTime fileDate)
        {
            return "Orders_" + DateTime.Today.ToString("MMddyyyy") + ".txt";
        }
        public List<Order> List(DateTime order)
        {
            List<Order> newList = new List<Order>();
            foreach(var x in _orderIndex)
            {
                newList.Add(x.Value);
            }
            return newList;
        }
    }
}

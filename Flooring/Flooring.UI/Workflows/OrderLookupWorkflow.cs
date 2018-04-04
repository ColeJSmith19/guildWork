using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flooring.BLL;
using Flooring.Data;
using Flooring.Models;
using Flooring.Models.Interfaces;
using Flooring.Models.Responses;

namespace Flooring.UI.Workflows
{
    public class OrderLookupWorkflow
    {
        public void Execute()
        {
            Console.Clear();
            var dateToSearch = ConsoleIO.GetDateToViewFromUser("Enter a date to view its orders");
            bool isAddWorkflow = false;
            Console.WriteLine("==========================================");
            //OrderRepository repo = new OrderRepository(dateToSearch, isAddWorkflow);
            IOrderRepository repo = OrderManagerFactory.Create(dateToSearch, isAddWorkflow);

            List<Order> orders = repo.List(dateToSearch);

            ConsoleIO.PrintAllOrdersForDate(orders);

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}

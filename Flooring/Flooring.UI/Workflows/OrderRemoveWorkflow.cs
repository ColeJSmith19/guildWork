using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flooring.BLL;
using Flooring.Data;
using Flooring.Models;
using Flooring.Models.Interfaces;

namespace Flooring.UI.Workflows
{
    public class OrderRemoveWorkflow
    {
        public void Execute()
        {
            bool isAddWorkflow = false;
            Console.Clear();
            Console.WriteLine("Remove Order");
            Console.WriteLine("-------------------------");

            DateTime orderDate = ConsoleIO.GetDateFromUser("Which date would you like to view?");

            //OrderRepository repo = new OrderRepository(orderDate, isAddWorkflow);
            IOrderRepository repo = OrderManagerFactory.Create(orderDate, isAddWorkflow);
            List<Order> orderList = repo.List(orderDate);
            ConsoleIO.PrintAllOrdersForDate(orderList);

            int orderNum = ConsoleIO.GetOrderNumberFromUser("Which order would you like to remove?", repo.GetOrderCount());

            Order OrderToRemove = repo.LoadOrder(orderNum.ToString());

            Console.WriteLine();

            Console.WriteLine(ConsoleIO.AddOrderLineFormat, OrderToRemove.OrderNumber, OrderToRemove.CustomerName, OrderToRemove.State, OrderToRemove.TaxRate, OrderToRemove.ProductType, OrderToRemove.Area, OrderToRemove.CostPerSquareFoot, OrderToRemove.LaborCostPerSquareFoot, OrderToRemove.MaterialCost, OrderToRemove.LaborCost, OrderToRemove.Tax, OrderToRemove.Total, OrderToRemove.OrderDate);
            Console.WriteLine();
            if (ConsoleIO.GetYesNoAnswerFromUser("Remove the selected order?") == "Y")
            {
                repo.Remove(orderNum.ToString(), orderDate);
                Console.WriteLine("Order removed");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }
            else
            {
                Console.WriteLine("Order remove cancelled");
                Console.WriteLine("Press any key to continue...");
                return;
            }
        }
    }
}

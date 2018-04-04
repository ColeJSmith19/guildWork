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
    public class OrderAddWorkflow
    {
        public void Execute()
        {
            bool isAddWorkflow = true;
            Console.Clear();
            Console.WriteLine("Add Order");
            Console.WriteLine("------------------");
            Console.WriteLine();
            Order newOrder = new Order();

            newOrder.OrderDate = ConsoleIO.GetDateFromUser("Order Date: ");
            newOrder.CustomerName = ConsoleIO.GetRequiredStringFromUser("Customer name: ");
            newOrder.State = ConsoleIO.GetRequiredStateFromUser("State: ");
            newOrder.TaxRate = ConsoleIO.StateAssociatedTax(newOrder.State);
            newOrder.ProductType = ConsoleIO.GetRequiredProductFromUser("Product Type: ");
            newOrder.Area = ConsoleIO.GetRequiredAreaFromUser("Area: ");
            newOrder.CostPerSquareFoot = ConsoleIO.ProductAssoiciatedCostPerSquareFoot(newOrder.ProductType);
            newOrder.LaborCostPerSquareFoot = ConsoleIO.ProductAssoiciatedLaborCost(newOrder.ProductType);
            newOrder.MaterialCost = newOrder.Area * newOrder.CostPerSquareFoot;
            newOrder.LaborCost = newOrder.Area * newOrder.LaborCostPerSquareFoot;
            newOrder.Tax = (newOrder.MaterialCost + newOrder.LaborCost) + (newOrder.TaxRate / 100M);
            newOrder.Total = newOrder.MaterialCost + newOrder.LaborCost + newOrder.Tax;
            //OrderRepository repo = new OrderRepository(newOrder.OrderDate, isAddWorkflow);
            IOrderRepository repo = OrderManagerFactory.Create(newOrder.OrderDate, isAddWorkflow);
            newOrder.OrderNumber = repo.GetOrderCount() + 1;

            Console.WriteLine(ConsoleIO.AddOrderLineFormat, newOrder.OrderNumber, newOrder.CustomerName, newOrder.State, newOrder.TaxRate, newOrder.ProductType, newOrder.Area, newOrder.CostPerSquareFoot, newOrder.LaborCostPerSquareFoot, newOrder.MaterialCost, newOrder.LaborCost, newOrder.Tax, newOrder.Total, newOrder.OrderDate.ToShortDateString());

            Console.WriteLine();
            if(ConsoleIO.GetYesNoAnswerFromUser("Add the following order?") == "Y")
            {           
                repo.Add(newOrder);           
                Console.WriteLine("Order added!");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }
            else
            {
                Console.WriteLine("Order cancelled");
                Console.WriteLine("Press any key to continue...");
                return;
            }
        }
    }
}

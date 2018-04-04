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
    public class OrderEditWorkflow
    {
        public void Execute()
        {
            bool isAddWorkflow = false;
            bool isEditWorkflow = true;
            string currentEditedValue;
            decimal currentEditedDecimal;
            Console.Clear();
            Console.WriteLine("Edit Order");
            Console.WriteLine("------------------");
            Console.WriteLine();

            DateTime orderDate = ConsoleIO.GetDateFromUser("Which date would you like to view?");

            //OrderRepository repo = new OrderRepository(orderDate, isAddWorkflow);
            IOrderRepository repo = OrderManagerFactory.Create(orderDate, isAddWorkflow);
            //break if no orders are on date
            Console.WriteLine();
            List<Order> orderList = repo.List(orderDate);
            ConsoleIO.PrintAllOrdersForDate(orderList);

            int orderNum = ConsoleIO.GetOrderNumberFromUser("Which order would you like to edit?", repo.GetOrderCount());


            Order OrderToEdit = repo.LoadOrder(orderNum.ToString());

            Console.WriteLine();

            currentEditedValue = ConsoleIO.GetRequiredStringFromUser(string.Format("Change name from {0} to: ", OrderToEdit.CustomerName), true);
            OrderToEdit.CustomerName = currentEditedValue == "" ? OrderToEdit.CustomerName : currentEditedValue; 
            currentEditedValue = ConsoleIO.GetRequiredStateFromUser(string.Format("Change state from {0} to: ", OrderToEdit.State), true);
            OrderToEdit.State = currentEditedValue == "" ? OrderToEdit.State : currentEditedValue;
            OrderToEdit.TaxRate = ConsoleIO.StateAssociatedTax(OrderToEdit.State);
            currentEditedValue = ConsoleIO.GetRequiredProductFromUser(string.Format("Change product type from {0} to: ", OrderToEdit.ProductType), true);
            OrderToEdit.ProductType = currentEditedValue == "" ? OrderToEdit.ProductType : currentEditedValue;
            currentEditedDecimal = ConsoleIO.GetRequiredAreaFromUser(string.Format("Change area from {0} square feet to: ", OrderToEdit.Area), true);
            OrderToEdit.Area = currentEditedDecimal == 0M ? OrderToEdit.Area : currentEditedDecimal;
            OrderToEdit.CostPerSquareFoot = ConsoleIO.ProductAssoiciatedCostPerSquareFoot(OrderToEdit.ProductType);
            OrderToEdit.LaborCostPerSquareFoot = ConsoleIO.ProductAssoiciatedLaborCost(OrderToEdit.ProductType);
            OrderToEdit.MaterialCost = OrderToEdit.Area * OrderToEdit.CostPerSquareFoot;
            OrderToEdit.LaborCost = OrderToEdit.Area * OrderToEdit.LaborCostPerSquareFoot;
            OrderToEdit.Tax = (OrderToEdit.MaterialCost + OrderToEdit.LaborCost) + (OrderToEdit.TaxRate / 100M);
            OrderToEdit.Total = OrderToEdit.MaterialCost + OrderToEdit.LaborCost + OrderToEdit.Tax;

            Console.WriteLine(ConsoleIO.EditOrderLineFormat, OrderToEdit.CustomerName, OrderToEdit.State, OrderToEdit.ProductType, OrderToEdit.Area);
            Console.WriteLine();
            if (ConsoleIO.GetYesNoAnswerFromUser("Edit the selected order?") == "Y")
            {
                repo.Edit(OrderToEdit, orderNum.ToString(), OrderToEdit.OrderDate);
                Console.WriteLine("Order updated");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }
            else
            {
                Console.WriteLine("Order edit cancelled");
                Console.WriteLine("Press any key to continue...");
                return;
            }
        }
    }
}

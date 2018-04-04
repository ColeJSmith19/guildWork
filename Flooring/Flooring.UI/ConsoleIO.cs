using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flooring.BLL;
using Flooring.Data;
using Flooring.Models;
using Flooring.Models.Interfaces;

namespace Flooring.UI
{
    public class ConsoleIO
    {
        public const string AddOrderLineFormat = "{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12}";
        public const string ViewOrderLineFormat = "{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}";
        public const string EditOrderLineFormat = "{0},{1},{2},{3}";

        public static void DisplayOrderDetails(Order order)
        {
            Console.WriteLine($"Order Number: {order.OrderNumber}");
            Console.WriteLine($"Customer Name: {order.CustomerName}");
            Console.WriteLine($"State: {order.State}");
            Console.WriteLine($"Tax Rate: {order.Tax}");
            Console.WriteLine($"Product Type: {order.ProductType}");
            Console.WriteLine($"Area: {order.Area} square feet");
            Console.WriteLine($"Cost per suqare foot: {order.CostPerSquareFoot:c}");
            Console.WriteLine($"Labor cost per square foot: {order.LaborCostPerSquareFoot:c}");
            Console.WriteLine($"Material cost: {order.MaterialCost:c}");
            Console.WriteLine($"Labor cost: {order.LaborCost:c}");
            Console.WriteLine($"Tax: {order.Tax:c}");
            Console.WriteLine($"Total: {order.Total:c}");
            Console.WriteLine($"Order Date: {order.OrderDate/*.ToString("DDmmyyyy")*/}");
        }

        public static void NoOrdersAtDatePrompt(DateTime dateToSearch)
        {
            Console.WriteLine($"There are no orders on {dateToSearch}");
        }

        public static string GetRequiredStringFromUser(string prompt, bool isEdit = false)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if(isEdit && input == "")
                {
                    return input;
                }
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("You must enter valid text.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    return input;
                }
            }
        }
        public static string GetRequiredStateFromUser(string prompt, bool isEdit = false)
        {
            TaxRepository stateRepo = new TaxRepository();
            while (true)
            {
                Console.Write(prompt);
                PrintAllStates(stateRepo.Taxes());
                Console.WriteLine("Please choose a state by abbreviation");
                List<string> stateChoices = new List<string>();
                foreach(var x in stateRepo.Taxes())
                {
                    stateChoices.Add(x.StateAbbreviation.Trim(' '));
                }
                return IsValidInput(stateChoices, isEdit);
            }
        }
        public static decimal StateAssociatedTax(string stateAbbreviation)
        {
            TaxRepository taxRepo = new TaxRepository();
            Dictionary<string,decimal> stateChoices = new Dictionary<string, decimal>();
            foreach (var x in taxRepo.Taxes())
            {                
                stateChoices.Add(x.StateAbbreviation, x.TaxRate);
            }
            return stateChoices[stateAbbreviation];
        }
        public static string GetRequiredProductFromUser(string prompt, bool isEdit = false)
        {
            ProductRepository productRepo = new ProductRepository();
            while (true)
            {
                Console.Write(prompt);
                PrintAllProducts(productRepo.Products());
                Console.WriteLine("Please choose a product by name");
                List<string> productChoices = new List<string>();
                foreach(var x in productRepo.Products())
                {
                    productChoices.Add(x.ProductType.ToUpper().Trim(' '));
                }
                return IsValidInput(productChoices, isEdit);
            }
        }
        public static decimal ProductAssoiciatedCostPerSquareFoot(string productType)
        {
            ProductRepository productRepo = new ProductRepository();
            Dictionary<string, decimal> productChoices = new Dictionary<string, decimal>();
            foreach(var x in productRepo.Products())
            {
                productChoices.Add(x.ProductType.ToUpper(), x.CostPerSquareFoot);
            }
            return productChoices[productType];
        }
        public static decimal ProductAssoiciatedLaborCost(string productType)
        {
            ProductRepository productRepo = new ProductRepository();
            Dictionary<string, decimal> productChoices = new Dictionary<string, decimal>();
            foreach (var x in productRepo.Products())
            {
                productChoices.Add(x.ProductType.ToUpper(), x.LaborCostPerSquareFoot);
            }
            return productChoices[productType];
        }
        public static string IsValidInput(List<string> validValues, bool isEdit)
        {
            bool finish = false;
            string input;
            do
            {
                input = Console.ReadLine().ToUpper();
                if(isEdit && input == "")
                {
                    return input;
                }
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("You must enter a value.");
                }
                if (!validValues.Contains(input))
                {
                    Console.WriteLine("You must enter a value from the list.");
                }
                else
                {
                    finish = true;
                }
            } while (!finish);
            return input;
        }
        public static decimal GetRequiredDecimalFromUser(string prompt)
        {
            decimal output;

            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (!decimal.TryParse(input, out output))
                {
                    Console.WriteLine("You must enter valid text.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    if (output < 0)
                    {
                        Console.WriteLine("Decimal values must be positive");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        continue;
                    }
                    return output;
                }
            }
        }
        public static decimal GetRequiredAreaFromUser(string prompt, bool isEdit = false)
        {
            decimal output;

            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (isEdit && input == "")
                {
                    return 0M;
                }
                if (!decimal.TryParse(input, out output))
                {
                    Console.WriteLine("You must enter a valid decimal.");
                }
                else
                {
                    if (output < 100)
                    {
                        Console.WriteLine("Area must be at least 100 square feet");
                        continue;
                    }
                    return output;
                }
            }
        }
        public static int GetRequiredIntFromUser(string prompt)
        {
            int output;

            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (!int.TryParse(input, out output))
                {
                    Console.WriteLine("You must enter valid text.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    if (output < 0)
                    {
                        Console.WriteLine("Integer values must be positive");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        continue;
                    }
                    return output;
                }
            }
        }
        public static DateTime GetDateFromUser(string prompt)
        {
            DateTime output;

            while (true)
            {
                Console.WriteLine(prompt);
                string input = Console.ReadLine();

                if(!DateTime.TryParse(input, out output))
                {
                    Console.WriteLine("You must enter a date.");
                    //Console.WriteLine("Press any key to continue...");
                    //Console.ReadKey();
                }
                else
                {
                    if(output < DateTime.Now)
                    {
                        Console.WriteLine("Dates must be in the future!");
                        //Console.WriteLine("Press any key to continue...");
                        //Console.ReadKey();
                        continue;
                    }
                    return output;
                }
            }
        }
        public static DateTime GetDateToViewFromUser(string prompt)
        {
            DateTime output;

            while (true)
            {
                Console.WriteLine(prompt);
                string input = Console.ReadLine();

                if (!DateTime.TryParse(input, out output))
                {
                    Console.WriteLine("You must enter a date.");
                    //Console.WriteLine("Press any key to continue...");
                    //Console.ReadKey();
                }
                else
                {
                    //if (output < DateTime.Now)
                    //{
                    //    Console.WriteLine("Dates must be in the future!");
                    //    //Console.WriteLine("Press any key to continue...");
                    //    //Console.ReadKey();
                    //    continue;
                    //}
                    return output;
                }
            }
        }
        public static string GetYesNoAnswerFromUser(string prompt)
        {
            while (true)
            {
                Console.WriteLine(prompt + "(Y/N)?");
                string input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("You must enter Y/N");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    if (input != "Y" && input != "N")
                    {
                        Console.WriteLine("You must enter Y/N");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        continue;
                    }
                    return input;
                }
            }
        }
        public static int GetOrderNumberFromUser(string prompt, int count)
        {
            int output;

            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (!int.TryParse(input, out output))
                {
                    Console.WriteLine("You must enter a valid interger.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    if (output < 1 || output > count)
                    {
                        Console.WriteLine("Please choose an order by number between {0} and {1}", 1, count);
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        continue;
                    }
                    return output;
                }
            }
        }
        public static void PrintPickOrder(Dictionary<string,Order> orders)
        {
            for (int i = 0; i < orders.Count(); i++)
            {
                Console.WriteLine(EditOrderLineFormat, i + 1, orders[i.ToString()].CustomerName + orders[i.ToString()].State, orders[i.ToString()].TaxRate, orders[i.ToString()].ProductType, orders[i.ToString()].Area, orders[i.ToString()].CostPerSquareFoot, orders[i.ToString()].LaborCostPerSquareFoot, orders[i.ToString()].MaterialCost, orders[i.ToString()].LaborCost, orders[i.ToString()].Tax, orders[i.ToString()].Total);
            }

            Console.WriteLine();
        }
        public static void PrintAllProducts(List<Product> productList)
        {
            Console.Clear();
            Console.WriteLine("Product Type - Cost Per Square Foot - Labor Cost Per Square Foot");
            Console.WriteLine("-----------------------------");
            foreach (var x in productList)
            {
                Console.WriteLine("{0} - ${1} - ${2}", x.ProductType, x.CostPerSquareFoot, x.LaborCostPerSquareFoot);
                Console.WriteLine("-----------------------------");
            }
        }
        public static void PrintAllStates(List<Tax> stateList)
        {
            Console.Clear();
            Console.WriteLine("State Abbreviation - State Name - Tax Rate");
            Console.WriteLine("We do not do business in non-listed states");
            Console.WriteLine("-----------------------------");
            foreach (var x in stateList)
            {
                Console.WriteLine("{0} - {1} - {2}",x.StateAbbreviation, x.StateName, x.TaxRate);
                Console.WriteLine("-----------------------------");
            }
        }
        public static void PrintAllOrdersForDate(List<Order> orders)
        {
            if(orders == null)
            {
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Menu.Start();
            }
            else if (orders.Count() > 0)
            {
                foreach (var x in orders)
                {
                    Console.WriteLine(ViewOrderLineFormat, x.OrderNumber, x.CustomerName, x.State, x.TaxRate, x.ProductType, x.Area, x.CostPerSquareFoot, x.LaborCostPerSquareFoot, x.MaterialCost, x.LaborCost, x.Tax, x.Total);
                }
            }
            else
            {
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Menu.Start();
            }
        }


    }

}

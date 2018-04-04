using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flooring.UI.Workflows;

namespace Flooring.UI
{
    public static class Menu
    {
        public static void Start()
        {
            while(true)
            {
                Console.Clear();
                Console.WriteLine("|||       SWC Flooring Project   |||");
                Console.WriteLine("|||------------------------------|||");
                Console.WriteLine("|||       1. Display Orders      |||");
                Console.WriteLine("|||------------------------------|||");
                Console.WriteLine("|||       2. Add Order           |||");
                Console.WriteLine("|||------------------------------|||");
                Console.WriteLine("|||       3. Edit Order          |||");
                Console.WriteLine("|||------------------------------|||");
                Console.WriteLine("|||       4. Remove Order        |||");
                Console.WriteLine("|||------------------------------|||");
                Console.WriteLine("|||       5. Display Products    |||");
                Console.WriteLine("|||------------------------------|||");
                Console.WriteLine("|||       6. Display all States  |||");
                Console.WriteLine("|||______________________________|||");

                Console.WriteLine("\nQ to Quit");
                Console.Write("\nEnter selection: ");

                string userinput = Console.ReadLine();

                switch (userinput)
                {
                    case "1":
                        OrderLookupWorkflow lookupWorkflow = new OrderLookupWorkflow();
                        lookupWorkflow.Execute();
                        break;
                    case "2":
                        OrderAddWorkflow addWorkflow = new OrderAddWorkflow();
                        addWorkflow.Execute();
                        break;
                    case "3":
                        OrderEditWorkflow editWorkflow = new OrderEditWorkflow();
                        editWorkflow.Execute();
                        break;
                    case "4":
                        OrderRemoveWorkflow removeWorkflow = new OrderRemoveWorkflow();
                        removeWorkflow.Execute();
                        break;
                    case "5":
                        DisplayProductsWorkflow displayWorkflow = new DisplayProductsWorkflow();
                        displayWorkflow.Execute();
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case "6":
                        DisplayAllStatesWorkflow statesWorkflow = new DisplayAllStatesWorkflow();
                        statesWorkflow.Execute();
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case "Q":
                        return;
                }
            }

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flooring.BLL;
using Flooring.Data;
using Flooring.Models;

namespace Flooring.UI.Workflows
{
    public class DisplayProductsWorkflow
    {
        public void Execute()
        {
            ProductManager myProductManager = new ProductManager();
            ConsoleIO.PrintAllProducts(myProductManager.GetAllProducts());
        }
    }
}

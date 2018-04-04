using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flooring.Data;
using Flooring.Models;

namespace Flooring.BLL
{
    public class ProductManager
    {
        public List<Product> GetAllProducts()
        {
            ProductRepository repo = new ProductRepository();
            return repo.Products();
        }        
    }
}

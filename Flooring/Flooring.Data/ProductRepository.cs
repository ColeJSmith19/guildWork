using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flooring.Models;

namespace Flooring.Data
{
    public class ProductRepository
    {
        public List<Product> productIndex;
        public const string productFile = @"C:\Cole Repo\cole-smith-individual-work\Flooring.UI\Data\Products.txt";
        //public const string productFile = @"D:\coleRepo\cole-smith-individual-work\Flooring.UI\Data\Products.txt";

        public List<Product> Products()
        {
            List<Product> productIndex = new List<Product>();
            using(StreamReader sr = new StreamReader(productFile))
            {
                string line;
                sr.ReadLine();
                while ((line = sr.ReadLine()) != null)
                {
                    Product currentProduct = new Product();

                    string[] columns = line.Split(',');
                    if(columns.Length >= 3)
                    {
                        currentProduct.ProductType = columns[0];
                        currentProduct.CostPerSquareFoot = decimal.Parse(columns[1]);
                        currentProduct.LaborCostPerSquareFoot = decimal.Parse(columns[2]);
                        productIndex.Add(currentProduct);
                    }
                }
                return productIndex;
            }
        }
    }
}

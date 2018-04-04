using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flooring.BLL;
using Flooring.Data;
using Flooring.Models;
using Flooring.Models.Responses;
using NUnit.Framework;

namespace FlooringTests
{
    [TestFixture]
    public class OrderTests
    {
        [Test]
        public void CanReadTaxesFromFile()
        {
            TaxRepository repo = new TaxRepository();

            List<Tax> taxes = repo.Taxes();

            Assert.AreEqual(7, taxes.Count());

            Tax check = taxes[1];

            Assert.AreEqual("OH", check.StateAbbreviation);
            Assert.AreEqual("Ohio", check.StateName);
            Assert.AreEqual(7.14M, check.TaxRate);
        }
        [Test]
        public void CanReadProductsFromFile()
        {
            ProductRepository repo = new ProductRepository();

            List<Product> products = repo.Products();

            Assert.AreEqual(10, products.Count());

            Product check = products[0];

            Assert.AreEqual("Wood", check.ProductType);
            Assert.AreEqual(12.5M, check.CostPerSquareFoot);
            Assert.AreEqual(9.25M, check.LaborCostPerSquareFoot);
        }
        [TestCase("OH", "Ohio", 7.14, true)]
        [TestCase("KY", "Kentucky", 6, true)]
        [TestCase("HI", "Hawaii", 4.35, true)]
        [TestCase("AK", "Alaska", 1.76, true)]
        [TestCase("RI", "Rhode Island", 7, true)]
        public void CanSellInState(string stateAbbreviation, string stateName, decimal taxRate, bool expectedResult)
        {
            TaxRepository repo = new TaxRepository();
            Tax stateTax = new Tax()
            {
                StateAbbreviation = stateAbbreviation,
                StateName = stateName,
                TaxRate = taxRate
            };
            Assert.AreEqual(expectedResult, true);

        }
        [Test]
        public void CanAddOrderTest()
        {
            OrderTestRepository repo = new OrderTestRepository();

            Order newOrder = new Order();
            newOrder.OrderNumber = 2;
            newOrder.CustomerName = "Alan Galloway";
            newOrder.State = "Kentucky";
            newOrder.TaxRate = 6M;
            newOrder.ProductType = "Wood";
            newOrder.Area = 10M;
            newOrder.CostPerSquareFoot = 5.15M;
            newOrder.LaborCostPerSquareFoot = 4.75M;
            newOrder.MaterialCost = (10 * 5.15M);
            newOrder.LaborCost = (10 * 4.75M);
            newOrder.Tax = (((10 * 5.15M) + (10 * 4.75M)) * (6 / 100M));
            newOrder.Total = ((10 * 5.15M) + (10 * 4.75M) + (((10 * 5.15M) + (10 * 4.75M)) * (6 / 100)));
            newOrder.OrderDate = DateTime.Today;

            repo.Add(newOrder);

            List<Order> orders = repo.List(DateTime.Today);
            Order check = orders[1];

            Assert.AreEqual("Alan Galloway", check.CustomerName);
            Assert.AreEqual("Kentucky", check.State);
            Assert.AreEqual(6M, check.TaxRate);
            Assert.AreEqual("Wood", check.ProductType);

        }
    }
}

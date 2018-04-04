using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Flooring.Models;
using Flooring.Models.Interfaces;

namespace Flooring.Data
{

    public class OrderRepository : IOrderRepository
    {
        private static Dictionary<string, Order> _orderIndex;
        public const string repoPath = @"C:\Cole Repo\cole-smith-individual-work\Flooring.UI\Data\";
        //public const string repoPath = @"D:\coleRepo\cole-smith-individual-work\Flooring.UI\Data\";

        public OrderRepository(DateTime fileDate, bool isAddWorkflow)
        {
            string repoFile = GetFileName(fileDate);
            _orderIndex = new Dictionary<string, Order>();
            if (!File.Exists(repoFile))
            {
                if (isAddWorkflow)
                {
                    using (File.Create(repoFile)) { };
                }
                else
                {
                    Console.WriteLine($"No orders for {fileDate} exist.");
                }
            }
            else
            {
                using (StreamReader sr = new StreamReader(repoFile))
                {
                    string line;
                    sr.ReadLine();
                    while ((line = sr.ReadLine()) != null)
                    {
                        //string[] columns = line.Split(',');
                        string[] columns = Regex.Split(line, @",(?!\s)");

                        if (columns.Length == 12)
                        {
                            string key = columns[0];
                            int orderNumber = int.Parse(columns[0]);
                            string customerName = columns[1];
                            //Regex.Split(customerName = columns[1], @",");
                            string state = columns[2];
                            decimal taxRate = decimal.Parse(columns[3]);
                            string productType = columns[4];
                            decimal area = decimal.Parse(columns[5]);
                            decimal costPerSquareFoot = decimal.Parse(columns[6]);
                            decimal laborCostPerSquareFoot = decimal.Parse(columns[7]);
                            decimal materialCost = decimal.Parse(columns[8]);
                            decimal laborCost = decimal.Parse(columns[9]);
                            decimal tax = decimal.Parse(columns[10]);
                            decimal total = decimal.Parse(columns[11]);
                            _orderIndex.Add(key, new Order()
                            {
                                OrderNumber = orderNumber,
                                CustomerName = customerName,
                                State = state,
                                TaxRate = taxRate,
                                ProductType = productType,
                                Area = area,
                                CostPerSquareFoot = costPerSquareFoot,
                                LaborCostPerSquareFoot = laborCostPerSquareFoot,
                                MaterialCost = materialCost,
                                LaborCost = laborCost,
                                Tax = tax,
                                Total = total,
                                OrderDate = fileDate
                            });
                        }
                    }
                }
            }
        }

        public void Edit(Order order, string key, DateTime editDate)
        {
            var orders = _orderIndex;
            orders[key] = order;
            CreateOrderFile(orders, editDate);
        }
        public static Dictionary<string, Order> ReturnOrderIndex()
        {
            var orderIndex = _orderIndex;
            return orderIndex;
        }

        public void Add(Order order)
        {
            _orderIndex.Add(order.OrderNumber.ToString(), order);
            CreateOrderFile(_orderIndex, order.OrderDate);
        }
        public int GetOrderCount() //Get Max order number from a file, set that equal to count and do count++ for new order numbers
        {
            if (_orderIndex.Count() == 0)
            {
                return 0;
            }
            else
            {
                return _orderIndex.Max(i => i.Value.OrderNumber);
            }

        }

        public void Remove(string key, DateTime removeDate)
        {
            var orders = _orderIndex;
            orders.Remove(key);
            CreateOrderFile(orders, removeDate);
        }

        public List<Order> List(DateTime order)
        {
            List<Order> orders = new List<Order>();
            try
            {
                using (StreamReader sr = new StreamReader(GetFileName(order)))
                {
                    sr.ReadLine();
                    string line;

                    while ((line = sr.ReadLine()) != null)
                    {
                        Order currentOrder = new Order();

                        //string[] columns = line.Split(',');
                        string[] columns = Regex.Split(line, @",(?!\s)");
                        
                        currentOrder.OrderNumber = int.Parse(columns[0]);
                        currentOrder.CustomerName = columns[1];
                        currentOrder.State = columns[2];
                        currentOrder.TaxRate = decimal.Parse(columns[3]);
                        currentOrder.ProductType = columns[4];
                        currentOrder.Area = decimal.Parse(columns[5]);
                        currentOrder.CostPerSquareFoot = decimal.Parse(columns[6]);
                        currentOrder.LaborCostPerSquareFoot = decimal.Parse(columns[7]);
                        currentOrder.MaterialCost = decimal.Parse(columns[8]);
                        currentOrder.LaborCost = decimal.Parse(columns[9]);
                        currentOrder.Tax = decimal.Parse(columns[10]);
                        currentOrder.Total = decimal.Parse(columns[11]);
                        currentOrder.OrderDate = order;

                        orders.Add(currentOrder);

                    }
                }
                return orders;
            }
            catch(FileNotFoundException)
            {
                Console.WriteLine("There is no local file for this date");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public static string CreateOrder(Order order)
        {
            return string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}", order.OrderNumber, order.CustomerName, order.State, order.TaxRate, order.ProductType, order.Area, order.CostPerSquareFoot, order.LaborCostPerSquareFoot, order.MaterialCost, order.LaborCost, order.Tax, order.Total);
        }
        private void CreateOrderFile(Dictionary<string, Order> order, DateTime fileDate)
        {
            if (File.Exists(GetFileName(fileDate)))
                File.Delete(GetFileName(fileDate));

                string repoFile = GetFileName(fileDate);

            using (StreamWriter sr = new StreamWriter(repoFile))
            {
                sr.WriteLine("OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total");
                foreach (var x in order)
                {
                    sr.WriteLine(CreateOrder(x.Value));
                }
            }          
        }
        public Order LoadOrder(string id)
        {
            if (_orderIndex.Keys.Contains(id))
            {
                return _orderIndex[id];
            }
            else
            {
                return null;
            }
        }
        private string GetFileName(DateTime fileDate)
        {
            return repoPath + "Orders_" + fileDate.ToString("MMddyyyy") + ".txt";
        }

    }
}

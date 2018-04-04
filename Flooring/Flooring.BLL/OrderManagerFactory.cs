using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flooring.Data;
using Flooring.Models.Interfaces;

namespace Flooring.BLL
{
    public static class OrderManagerFactory
    {
        public static IOrderRepository Create(DateTime orderDate, bool isAddWorkflow)
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();

            switch (mode)
            {
                case "Test":
                    return new OrderTestRepository();
                case "Production":
                    return new OrderRepository(orderDate, isAddWorkflow);
                default:
                    throw new Exception("Mode value in app config is not valid.");
            }
        }
    }
}

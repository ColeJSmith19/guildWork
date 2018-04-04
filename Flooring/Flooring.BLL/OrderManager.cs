using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flooring.Models.Interfaces;
using Flooring.Models.Responses;

namespace Flooring.BLL
{
    public class OrderManager
    {
        private IOrderRepository _orderRepository;

        public OrderManager(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public OrderLookupResponse LookUpOrder(string orderNumber)
        {
            OrderLookupResponse response = new OrderLookupResponse();

            response.Order = _orderRepository.LoadOrder(orderNumber);

            if(response.Order == null)
            {
                response.Success = false;
                response.Message = $"{orderNumber} is not a valid order number.";
            }
            else
            {
                response.Success = true;
            }
            return response;
        }
    }
}

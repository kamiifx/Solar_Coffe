using System.Collections.Generic;
using SolarCoffe.Data.Models;

namespace SolarCoffe.Services.Order
{
    public class OrderService : IOrderService
    {
        public List<SalesOrder> GetOrders()
        {
            throw new System.NotImplementedException();
        }

        public ServiceResponse<bool> GenerateInvoiceForOrder(SalesOrder order)
        {
            throw new System.NotImplementedException();
        }

        public ServiceResponse<bool> MarkFulfilled(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
using System.Collections.Generic;
using SolarCoffe.Data.Models;

namespace SolarCoffe.Services.Order
{
    public interface IOrderService
    {
        List<SalesOrder> GetOrders();
        ServiceResponse<bool> GenerateInvoiceForOrder(SalesOrder order);
        ServiceResponse<bool> MarkFulfilled(int id);
    }
}
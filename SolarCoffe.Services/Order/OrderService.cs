using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SolarCoffe.Data;
using SolarCoffe.Data.Models;
using SolarCoffe.Services.Inventory;
using SolarCoffe.Services.Product;

namespace SolarCoffe.Services.Order
{
    public class OrderService : IOrderService
    {
        private readonly SolarDbContex _db;
        private readonly ILogger<OrderService> _logger;
        private readonly ProductService _productService;
        private readonly InventoryService _inventoryService;
        public OrderService(SolarDbContex db, ILogger<OrderService> logger, ProductService prodServe, InventoryService invServe )
        {
            _db = db;
            _logger = logger;
            _productService = prodServe;
            _inventoryService = invServe;
        }
        public List<SalesOrder> GetOrders()
        {
            return _db.SalesOrders
                .Include(so => so.Customer)
                    .ThenInclude(cu => cu.PrimaryAdress)
                .Include(so => so.SalesOrderItems)
                    .ThenInclude(sa => sa.Product)
                .ToList();
        }

        public ServiceResponse<bool> GenerateInvoiceForOrder(SalesOrder order)
        {
            foreach (var item in order.SalesOrderItems)
            {
                item.Product = _productService.GetProductById(item.Product.Id);
                item.Quantity = item.Quantity;
                var inventoryId = _inventoryService.GetByProductId(item.Product.Id).Id;

                _inventoryService.UpdateUnitAvailable(inventoryId, -item.Quantity);
            }
        }

        public ServiceResponse<bool> MarkFulfilled(int id)
        {
            try
            {
                var order = _db.SalesOrders.Find(id);
                order.IsPaid = true;
                _db.SaveChanges();

                return new ServiceResponse<bool>
                {
                    Data = true,
                    IsSuccess = true,
                    Message = "Order marked paid",
                    Time = DateTime.UtcNow
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<bool>
                {
                    Data = false,
                    IsSuccess = false,
                    Message = "Error marking order : " + e.StackTrace,
                    Time = DateTime.UtcNow
                };
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using SolarCoffe.Data;
using SolarCoffe.Data.Models;

namespace SolarCoffe.Services.Product
{
    public class ProductService : IProductService
    {
        private readonly SolarDbContex _db;

        public ProductService(SolarDbContex dbContex)
        {
            _db = dbContex;
        }
        
        public List<Data.Models.Product> GetAllProducts()
        {
            return _db.Products.ToList();
        }

        public Data.Models.Product GetProductById(int id)
        {
            return _db.Products.Find(id);
        }

        public ServiceResponse<bool> CreateProduct(Data.Models.Product product)
        {
            try
            {
                _db.Products.Add(product);
                var newInventory = new ProductInventory
                {
                    Product = product,
                    QuantityOnHand = 0,
                    IdealQuantity = 10
                };
                _db.ProductInventories.Add(newInventory);
                _db.SaveChanges();

                return new ServiceResponse<bool>
                {
                    Data = true,
                    Time = DateTime.UtcNow,
                    Message = "Saved New Product",
                    IsSuccess = true
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<bool>
                {
                    Data = false,
                    Time = DateTime.UtcNow,
                    Message = "Error Saving New Product",
                    IsSuccess = false
                };

            }

        }

        public ServiceResponse<bool> ArchiveProduct(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
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

        public ProductService(SolarDbContex db)
        {
            _db = db;
        }

        public List<Data.Models.Product> GetAllProducts()
        {
            return _db.Products.ToList();
        }

        public Data.Models.Product GetProductById(int id)
        {
            return _db.Products.Find(id);
        }
        
        
        public ServiceResponse<Data.Models.Product> CreateProduct(Data.Models.Product product)
        {
            try
            {
                _db.Products.Add(product);

                var newInventory = new ProductInventory
                {
                    Product = product,
                    QuantityOnHand = 0,
                    IdealQuantity = 10,
                };
                _db.ProductInventories.Add(newInventory);
                
                _db.SaveChanges();

                return new ServiceResponse<Data.Models.Product>
                {
                    Data = product,
                    IsSuccess = true,
                    Time = DateTime.UtcNow,
                    Message = "Saved new product"
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Data.Models.Product>
                {
                    Data = product,
                    Time = DateTime.UtcNow,
                    IsSuccess = false,
                    Message = "Error saving product"
                };
            }
        }

        public ServiceResponse<Data.Models.Product> ArchiveProduct(int id)
        {
            try
            {
                var archProduct = GetProductById(id);
                archProduct.IsArchived = true;
                _db.SaveChanges();

                return new ServiceResponse<Data.Models.Product>
                {
                    Data = archProduct,
                    Time = DateTime.UtcNow,
                    IsSuccess = true,
                    Message = "Product Successfully removed"
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Data.Models.Product>
                {
                    Data = null,
                    Time = DateTime.UtcNow,
                    IsSuccess = false,
                    Message = "Error removing product : " + e.StackTrace
                };
            }
        }
    }
}
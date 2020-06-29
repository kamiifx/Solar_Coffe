using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SolarCoffe.Data;
using SolarCoffe.Data.Models;

namespace SolarCoffe.Services.Inventory
{
    public class InventoryService : IInventoryService
    {
        private readonly SolarDbContex _db;
        private readonly ILogger<InventoryService> _logger;

        public InventoryService(SolarDbContex db, ILogger<InventoryService> logger)
        {
            _db = db;
            _logger = logger;
        }
        public List<ProductInventory> GetCurrentInventory()
        {
            return _db.ProductInventories.Include(prodinv => prodinv.Product)
                .Where(prodinv => !prodinv.Product.IsArchived).ToList();
        }

        public ServiceResponse<ProductInventory> UpdateUnitAvailable(int id, int adjustment)
        {
            try
            {
                var inventory = _db.ProductInventories
                    .Include(inv => inv.Product)
                    .First(inv => inv.Product.Id == id);
                inventory.QuantityOnHand += adjustment;

                try
                {
                    CreateSnapShot(inventory);
                }
                catch (Exception e)
                {
                    _logger.LogInformation("Failed creating snapshot: ");
                    _logger.LogInformation(e.StackTrace);
                }

                _db.SaveChanges();

                return new ServiceResponse<ProductInventory>
                {
                    Data = inventory,
                    IsSuccess = true,
                    Message = "Inventory Updated",
                    Time = DateTime.UtcNow
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<ProductInventory>
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "Error updating : " + e.StackTrace,
                    Time = DateTime.UtcNow
                };
            }
        }

        public ProductInventory GetByProductId(int productId)
        {
            return _db.ProductInventories
                .Include(prodInv => prodInv.Product)
                .FirstOrDefault(prodInv => prodInv.Id == productId);
        }
        
        public List<ProductInventorySnapshot> GetSnapshotHistory()
        {
            var earliest = DateTime.UtcNow - TimeSpan.FromHours(6);
            return _db.ProductInventorySnapshots.Include(snap => snap.Product)
                .Where(snap => snap.SnapShotTime > earliest && !snap.Product.IsArchived).ToList();
        }

        private void CreateSnapShot(ProductInventory inventory)
        {
            var snapshot = new ProductInventorySnapshot
            {
                SnapShotTime = DateTime.UtcNow,
                Product = inventory.Product,
                QuantityOnHand = inventory.QuantityOnHand
            };
            _db.Add(snapshot);
        }
    }
}
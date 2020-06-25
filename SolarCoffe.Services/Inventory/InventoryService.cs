using System.Collections.Generic;
using SolarCoffe.Data.Models;

namespace SolarCoffe.Services.Inventory
{
    public class InventoryService : IInventoryService
    {
        public List<ProductInventory> GetCurrentInventory()
        {
            throw new System.NotImplementedException();
        }

        public ServiceResponse<ProductInventory> UpdateUnitAvailable(int id, int adjustment)
        {
            throw new System.NotImplementedException();
        }

        public ProductInventory GetByProductId(int productId)
        {
            throw new System.NotImplementedException();
        }

        public void CreateSnapShot()
        {
            throw new System.NotImplementedException();
        }

        public List<ProductInventorySnapshot> GetSnapshotHistory()
        {
            throw new System.NotImplementedException();
        }
    }
}
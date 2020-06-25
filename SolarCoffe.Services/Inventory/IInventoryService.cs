using System.Collections.Generic;
using SolarCoffe.Data.Models;

namespace SolarCoffe.Services.Inventory
{
    public interface IInventoryService
    {
        List<ProductInventory> GetCurrentInventory();
        ServiceResponse<ProductInventory> UpdateUnitAvailable(int id, int adjustment);
        ProductInventory GetByProductId(int productId);
        void CreateSnapShot();
        List<ProductInventorySnapshot> GetSnapshotHistory();

    }
}
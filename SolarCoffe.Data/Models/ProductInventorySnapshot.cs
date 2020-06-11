using System;

namespace SolarCoffe.Data.Models
{
    public class ProductInventorySnapshot
    {
        public int Id { get; set; }
        public DateTime SnapShotTime { get; set; }
        public int QuantityOnHand { get; set; }
        public Product Product { get; set; }
    }
}
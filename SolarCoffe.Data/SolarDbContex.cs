using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SolarCoffe.Data.Models;

namespace SolarCoffe.Data
{
    public class SolarDbContex : IdentityDbContext
    {
        public SolarDbContex(){}
        
        public SolarDbContex(DbContextOptions options) : base(options){}
        
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerAdress> CustomerAdresses { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductInventory> ProductInventories { get; set; }
        public virtual DbSet<ProductInventorySnapshot> ProductInventorySnapshots { get; set; }
        public virtual DbSet<SalesOrder> SalesOrders { get; set; }
        public virtual DbSet<SalesOrderItem> SalesOrderItems { get; set; }
    }
}
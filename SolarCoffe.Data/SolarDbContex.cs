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
    }
}
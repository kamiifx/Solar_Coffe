using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SolarCoffe.Data;

namespace SolarCoffe.Services.Customer
{
    public class CustomerService : ICustomerService
    {
        private readonly SolarDbContex _db;

        public CustomerService(SolarDbContex db)
        {
            _db = db;
        }
        
        public List<Data.Models.Customer> GetAllCustomers()
        {
            return _db.Customers.Include(customer => customer.PrimaryAdress).OrderBy(customer => customer.LastName)  //Include = Including CustomerAddresses with Customers. 
                .ToList();
        }

        public ServiceResponse<Data.Models.Customer> CreateCustomer(Data.Models.Customer customer)
        {
            try
            {
                _db.Customers.Add(customer);
                _db.SaveChanges();
                return new ServiceResponse<Data.Models.Customer>
                {
                    Data = customer,
                    Time = DateTime.UtcNow,
                    IsSuccess = true,
                    Message = "Successfully added customer"
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Data.Models.Customer>
                {
                    Data = null,
                    Time = DateTime.UtcNow,
                    IsSuccess = false,
                    Message = "Error adding Customer" + e.StackTrace
                };
            }
        }

        public ServiceResponse<bool> ArchiveCustomer(int id)
        {
            var customerArch = GetCustomerById(id);
            if (customerArch == null)
            {
                return new ServiceResponse<bool>
                {
                    Time = DateTime.UtcNow,
                    IsSuccess = false,
                    Message = "Customer to delete not found",
                    Data = false
                };
            }

            try
            {
                _db.Customers.Remove(customerArch);
                _db.SaveChanges();

                return new ServiceResponse<bool>
                {
                    Data = true,
                    Time = DateTime.UtcNow,
                    IsSuccess = true,
                    Message = "Successfully deleted customer"
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<bool>
                {
                    Data = false,
                    IsSuccess = false,
                    Message = "Error deleting customer" + e.StackTrace,
                    Time = DateTime.UtcNow
                };
            }
        }

        public Data.Models.Customer GetCustomerById(int id)
        {
            return _db.Customers.Find(id);
        }
    }
}
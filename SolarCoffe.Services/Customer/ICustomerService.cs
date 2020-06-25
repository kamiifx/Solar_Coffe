using System.Collections.Generic;

namespace SolarCoffe.Services.Customer
{
    public interface ICustomerService
    {
        List<Data.Models.Customer> GetAllCustomers();
        ServiceResponse<Data.Models.Customer> CreateCustomer(Data.Models.Customer customer);
        ServiceResponse<bool> ArchiveCustomer(int id);
        Data.Models.Customer GetCustomerById(int id);
    }
}
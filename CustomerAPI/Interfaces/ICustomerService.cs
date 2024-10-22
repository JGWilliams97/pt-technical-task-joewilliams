using CustomerAPI.Models;

namespace CustomerAPI.Interfaces
{
    public interface ICustomerService
    {
        public Task<List<Customer>?> GetCustomers();
        public Task<Customer> GetCustomerById(int id);
        public Task<int> AddCustomer(Customer customer);
        public Task<int> UpdateCustomer(Customer customer);
        public Task<int> DeleteCustomer(int customerId);
    }
}

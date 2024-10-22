using CustomerAPI.Helpers;
using CustomerAPI.Interfaces;
using CustomerAPI.Models;
using System.Data;
using System.Data.SqlClient;

namespace CustomerAPI.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IConfiguration _configuration;
        public CustomerService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<Customer>?> GetCustomers()
        {
            try
            {
                List<Customer> customers = new List<Customer>();
                var test = _configuration.GetConnectionString("DefaultConnection");
                using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionStringWithDataDirectory("DefaultConnection")))
                {

                    await conn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("GetCustomers", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dataTable = new DataTable();
                            await Task.Run(() => adapter.Fill(dataTable));

                            foreach (DataRow row in dataTable.Rows)
                            {
                                var customer = new Customer
                                {
                                    Id = (int)row["Id"],
                                    Name = row["CustomerName"].ToString(),
                                    Email = row["CustomerEmail"].ToString()
                                };
                                customers.Add(customer);
                            }
                        }
                    }
                    return customers;
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"SQL Error: {sqlEx.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            List<Customer> customers = new List<Customer>();
            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionStringWithDataDirectory("DefaultConnection")))
            {
                try
                {
                    await conn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("GetCustomers", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", id);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dataTable = new DataTable();
                            await Task.Run(() => adapter.Fill(dataTable));

                            foreach (DataRow row in dataTable.Rows)
                            {
                                var customer = new Customer
                                {
                                    Id = (int)row["Id"],
                                    Name = row["CustomerName"].ToString(),
                                    Email = row["CustomerEmail"].ToString()
                                };
                                customers.Add(customer);
                            }
                        }
                    }
                    return customers.FirstOrDefault();
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine($"SQL Error: {sqlEx.Message}");
                    return null;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    return null;
                }
            }
        }


        public async Task<int> AddCustomer(Customer customer)
        {
            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionStringWithDataDirectory("DefaultConnection")))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("AddCustomer", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@CustomerName", customer.Name);
                        cmd.Parameters.AddWithValue("@CustomerEmail", customer.Email);

                        await cmd.ExecuteNonQueryAsync();

                        return 0;
                    }
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine($"SQL Error: {sqlEx.Message}");
                    return sqlEx.ErrorCode;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    return -1;
                }
            }
        }

        public async Task<int> UpdateCustomer(Customer customer)
        {
            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionStringWithDataDirectory("DefaultConnection")))
            {
                try
                {
                    await conn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("UpdateCustomer", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Id", customer.Id);
                        cmd.Parameters.AddWithValue("@CustomerName", customer.Name);
                        cmd.Parameters.AddWithValue("@CustomerEmail", customer.Email);

                        await cmd.ExecuteNonQueryAsync();
                        return 0;
                    }
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine($"SQL Error: {sqlEx.Message}");
                    return sqlEx.ErrorCode;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    return -1;
                }
            }
        }

        public async Task<int> DeleteCustomer(int id)
        {
            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionStringWithDataDirectory("DefaultConnection")))
            {
                try
                {
                    await conn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("DeleteCustomer", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Id", id);

                        await cmd.ExecuteNonQueryAsync();
                        return 0;
                    }
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine($"SQL Error: {sqlEx.Message}");
                    return sqlEx.ErrorCode;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    return -1;
                }
            }
        }
    }
}

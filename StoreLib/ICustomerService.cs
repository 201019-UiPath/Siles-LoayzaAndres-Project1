using System.Collections.Generic;
using StoreDB.Models;

namespace StoreLib
{
    public interface ICustomerService : ILocationService
    {
        void SignUpNewCustomer(Customer customer);
        Customer SignInExistingCustomer(string username, string password);
        List<Order> GetOrdersByCostAscend(int customerId);
        List<Order> GetOrdersByCostDescend(int customerId);
        List<Order> GetOrdersByDateAscend(int customerId);
        List<Order> GetOrdersByDateDescend(int customerId);
    }
}
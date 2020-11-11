using System.Collections.Generic;
using StoreDB.Models;

namespace StoreLib
{
    interface IOrderService
    {
        List<Order> GetCustomerOrdersByCostAscend(int customerId);
        List<Order> GetCustomerOrdersByCostDescend(int customerId);
        List<Order> GetCustomerOrdersByDateAscend(int customerId);
        List<Order> GetCustomerOrdersByDateDescend(int customerId);
        List<Order> GetLocationOrdersByCostAscend(int locationId);
        List<Order> GetLocationOrdersByCostDescend(int locationId);
        List<Order> GetLocationOrdersByDateAscend(int locationId);
        List<Order> GetLocationOrdersByDateDescend(int locationId);
    }
}
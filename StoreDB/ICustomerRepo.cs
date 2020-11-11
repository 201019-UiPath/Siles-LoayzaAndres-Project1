using System;
using System.Collections.Generic;
using StoreDB.Models;

namespace StoreDB
{
    public interface ICustomerRepo
    {
        Customer GetDefaultCustomer();
        List<Order> GetOrdersAscend(Func<Order, bool> where, Func<Order, Object> orderBy);
        List<Order> GetOrdersDescend(Func<Order, bool> where, Func<Order, Object> orderBy);
        List<Location> GetLocations();
        List<OrderItem> GetOrderItems(int orderId);
    }
}
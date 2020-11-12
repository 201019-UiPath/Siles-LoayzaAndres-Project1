using System;
using System.Collections.Generic;
using StoreDB.Models;

namespace StoreDB
{
    public interface ICustomerRepo
    {
        void AddCustomer(Customer customer);
        Customer GetCustomer(string username);
        bool HasCustomer(string username);
        List<Order> GetOrdersAscend(Func<Order, bool> where, Func<Order, Object> orderBy);
        List<Order> GetOrdersDescend(Func<Order, bool> where, Func<Order, Object> orderBy);
        List<OrderItem> GetOrderItems(int orderId);
    }
}
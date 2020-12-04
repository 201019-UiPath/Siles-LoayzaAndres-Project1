using System;
using System.Collections.Generic;
using StoreDB.Models;

namespace StoreDB
{
    public interface ICustomerRepo
    {
        /// <summary>
        /// Adds the specified customer to the database and saves changes.
        /// </summary>
        /// <param name="customer"></param>
        void AddCustomer(Customer customer);
        /// <summary>
        /// Returns the customer stored in the database with the specified 
        /// username.
        /// </summary>
        /// <param name="username"></param>
        /// <returns>a customer</returns>
        Customer GetCustomer(string username);
        /// <summary>
        /// Returns true if the database has a customer with the specified
        /// username. Returns false otherwise.
        /// </summary>
        /// <param name="username"></param>
        /// <returns>a bool</returns>
        bool HasCustomer(string username);
        /// <summary>
        /// Returns all orders stored in the database with the specified
        /// conditions and in the specified order (ascending).
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <returns>a list of orders</returns>
        List<Order> GetOrdersAscend(Func<Order, bool> where, Func<Order, Object> orderBy);
        /// <summary>
        /// Returns all orders stored in the database with the specified 
        /// conditions and in the specified order (descending).
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <returns>a list of orders</returns>
        List<Order> GetOrdersDescend(Func<Order, bool> where, Func<Order, Object> orderBy);
        /// <summary>
        /// Returns all order items stored in the database with the specified
        /// order ID.
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns>a list of order items</returns>
        List<OrderItem> GetOrderItems(int orderId);
    }
}
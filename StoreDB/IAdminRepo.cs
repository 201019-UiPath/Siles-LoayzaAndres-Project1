using System;
using System.Collections.Generic;
using StoreDB.Models;

namespace StoreDB
{
    /// <summary>
    /// Provides methods for accessing the store database and making changes
    /// on the admin level. Includes adding locations and inventory; and
    /// getting location orders.
    /// </summary>
    public interface IAdminRepo
    {
        /// <summary>
        /// Adds the specified location to the database and saves changes.
        /// </summary>
        /// <param name="location"></param>
        void AddLocation(Location location);
        /// <summary>
        /// Adds the specified inventory item to the database and saves 
        /// changes.
        /// </summary>
        /// <param name="invItem"></param>
        void AddInvItem(InvItem invItem);
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
        /// <summary>
        /// Adds to the quantity of the inventory item in the database with the
        /// specified location ID and product ID. Saves changes.
        /// </summary>
        /// <param name="locationId"></param>
        /// <param name="productId"></param>
        /// <param name="addend"></param>
        void AddToInvItemQuantity(int locationId, int productId, int addend);
    }
}
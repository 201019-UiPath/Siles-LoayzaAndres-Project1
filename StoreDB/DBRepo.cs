using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using StoreDB.Models;

namespace StoreDB
{
    /// <summary>
    /// Implementation of IRepo methods using a database management system.
    /// Uses EF Core and StoreContext to store and access data for the store.
    /// </summary>
    public class DBRepo : IStoreRepo
    {
        /// <summary>
        /// The StoreContext for this repo. Inherits from DbContext. Provides
        /// data access for the database.
        /// </summary>
        StoreContext context;

        /// <summary>
        /// Constructor that sets the specified context.
        /// </summary>
        /// <param name="context"></param>
        public DBRepo(StoreContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Adds the specified cart to the database and saves changes.
        /// </summary>
        /// <param name="cart"></param>
        public void AddCart(Cart cart)
        {
            context.Carts.Add(cart);
            context.SaveChanges();
        }

        /// <summary>
        /// Adds the specified customer to the database and saves changes.
        /// </summary>
        /// <param name="customer"></param>
        public void AddCustomer(Customer customer)
        {
            context.Customers.Add(customer);
            context.SaveChanges();
        }

        /// <summary>
        /// Adds the specified location to the database and saves changes.
        /// </summary>
        /// <param name="location"></param>
        public void AddLocation(Location location)
        {
            context.Locations.Add(location);
            context.SaveChanges();
        }

        /// <summary>
        /// Adds the specified inventory item to the database and saves 
        /// changes.
        /// </summary>
        /// <param name="invItem"></param>
        public void AddInvItem(InvItem invItem)
        {
            context.InvItems.Add(invItem);
            context.SaveChanges();
        }

        /// <summary>
        /// Adds the specified cart item to the database and saves changes.
        /// </summary>
        /// <param name="cartItem"></param>
        public void AddCartItem(CartItem cartItem)
        {
            context.CartItems.Add(cartItem);
            context.SaveChanges();
        }

        /// <summary>
        /// Removes all items stored in the cart with the specified id from the
        /// database. Saves changes.
        /// </summary>
        /// <param name="cartId"></param>
        public void EmptyCart(int cartId)
        {
            context.Carts.Include("Items").Single(x => x.Id==cartId).Items.Clear();
            context.SaveChanges();
        }

        /// <summary>
        /// Returns the cart stored in the database with the specified customer
        /// ID and location ID.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="locationId"></param>
        /// <returns>a cart</returns>
        public Cart GetCart(int customerId, int locationId)
        {
            return context.Carts.Single(x => x.CustomerId == customerId && x.LocationId == locationId);
        }

        /// <summary>
        /// Returns the cart stored in the database with the specified cart ID.
        /// </summary>
        /// <param name="cartId"></param>
        /// <returns>a cart</returns>
        public Cart GetCart(int cartId)
        {
            return context.Carts.Single(x => x.Id == cartId);
        }

        /// <summary>
        /// Returns the cart item stored in the database with the specified 
        /// cart ID and product ID.
        /// </summary>
        /// <param name="cartId"></param>
        /// <param name="productId"></param>
        /// <returns>a cart item</returns>
        public CartItem GetCartItem(int cartId, int productId)
        {
            return context.CartItems.Single(x => x.CartId==cartId && x.ProductId==productId);
        }

        /// <summary>
        /// Returns all cart items stored in the database with the specified
        /// cart ID.
        /// </summary>
        /// <param name="cartId"></param>
        /// <returns>a list of cart items</returns>
        public List<CartItem> GetCartItems(int cartId)
        {
            return context.CartItems.Include("Product").Where(x => x.CartId==cartId).ToList();
        }

        /// <summary>
        /// Returns the customer stored in the database with the specified 
        /// username.
        /// </summary>
        /// <param name="username"></param>
        /// <returns>a customer</returns>
        public Customer GetCustomer(string username)
        {
            return context.Customers.Single(x => x.UserName==username);
        }

        /// <summary>
        /// Returns the inventory item stored in the database with the 
        /// specified location ID and product ID.
        /// </summary>
        /// <param name="locationId"></param>
        /// <param name="productId"></param>
        /// <returns>an inventory item</returns>
        public InvItem GetInvItem(int locationId, int productId)
        {
            return context.InvItems.Include("Product").Single(x => x.LocationId==locationId && x.ProductId==productId);
        }

        /// <summary>
        /// Returns all inventory items stored in the database with the 
        /// specified location ID.
        /// </summary>
        /// <param name="locationId"></param>
        /// <returns>a list of inventory items</returns>
        public List<InvItem> GetInvItemsByLocation(int locationId)
        {
            return context.InvItems.Include("Product").Where(x => x.LocationId == locationId).ToList();
        }

        /// <summary>
        /// Returns all location stored in the database.
        /// </summary>
        /// <returns>a list of locations</returns>
        public List<Location> GetLocations()
        {
            return context.Locations.Include("Address").Select(x => x).ToList();
        }

        /// <summary>
        /// Returns all orders stored in the database with the specified
        /// conditions and in the specified order (ascending).
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <returns>a list of orders</returns>
        public List<Order> GetOrdersAscend(Func<Order, bool> where, Func<Order, Object> orderBy)
        {
            return context.Orders.Include("DestinationAddress")
                                 .Where(where)
                                 .OrderBy(orderBy)
                                 .ToList();
        }

        /// <summary>
        /// Returns all orders stored in the database with the specified 
        /// conditions and in the specified order (descending).
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <returns>a list of orders</returns>
        public List<Order> GetOrdersDescend(Func<Order, bool> where, Func<Order, Object> orderBy)
        {
            return context.Orders.Include("DestinationAddress")
                                 .Where(where)
                                 .OrderByDescending(orderBy)
                                 .ToList();
        }

        /// <summary>
        /// Returns all order items stored in the database with the specified
        /// order ID.
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns>a list of order items</returns>
        public List<OrderItem> GetOrderItems(int orderId)
        {
            return context.OrderItems.Include("Product").Where(x => x.OrderId==orderId).ToList();
        }

        /// <summary>
        /// Returns true if the database has a cart with the specified customer
        /// ID and location ID. Returns false otherwise.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="locationId"></param>
        /// <returns>a bool</returns>
        public bool HasCart(int customerId, int locationId)
        {
            return context.Carts.Any(x => x.CustomerId==customerId && x.LocationId==locationId);
        }

        /// <summary>
        /// Returns true if the database has a cart item with the cart ID and
        /// the product ID of the specified cart item. Returns false otherwise.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>a bool</returns>
        public bool HasCartItem(CartItem item)
        {
            return context.CartItems.Any(x => x.CartId==item.CartId && x.ProductId==item.ProductId);
        }

        /// <summary>
        /// Returns true if the database has a customer with the specified
        /// username. Returns false otherwise.
        /// </summary>
        /// <param name="username"></param>
        /// <returns>a bool</returns>
        public bool HasCustomer(string username)
        {
            return context.Customers.Any(x => x.UserName==username);
        }

        /// <summary>
        /// Adds the specified order to the database. Finds all inventory items
        /// in the database that match with the specified location and products
        /// in the specified cart. Reduces the quantity of those inventory 
        /// items by the quantity of the matching cart items. Empties the 
        /// specified cart. Performs these actions as a single transaction.
        /// </summary>
        /// <param name="locationId"></param>
        /// <param name="cartId"></param>
        /// <param name="order"></param>
        public void PlaceOrderTransaction(int locationId, int cartId, Order order)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    foreach(CartItem item in GetCartItems(cartId))
                    {
                        GetInvItem(locationId, item.ProductId).Quantity -= item.Quantity;
                    }
                    context.SaveChanges();
                    EmptyCart(cartId);
                    context.SaveChanges();
                    context.Orders.Add(order);
                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// Removes the specified cart item from the database. Saves changes.
        /// </summary>
        /// <param name="item"></param>
        public void RemoveCartItem(CartItem item)
        {
            context.CartItems.Remove(item);
            context.SaveChanges();
        }

        /// <summary>
        /// Adds to the quantity of the inventory item in the database with the
        /// specified location ID and product ID. Saves changes.
        /// </summary>
        /// <param name="locationId"></param>
        /// <param name="productId"></param>
        /// <param name="addend"></param>
        public void AddToInvItemQuantity(int locationId, int productId, int addend)
        {
            GetInvItem(locationId, productId).Quantity += addend;
            context.SaveChanges();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StoreDB.Models;

namespace StoreDB
{
    /// <summary>
    /// Implementation of IRepo methods using a database management system.
    /// Uses EF Core and DbContext to store and access data for the store.
    /// </summary>
    public class DBRepo : IRepo
    {
        StoreContext context;

        public DBRepo(StoreContext context)
        {
            this.context = context;
        }

        public void AddCart(Cart cart)
        {
            context.Carts.Add(cart);
            context.SaveChanges();
        }

        public void AddLocation(Location location)
        {
            context.Locations.Add(location);
            context.SaveChanges();
        }

        public void AddInvItem(InvItem invItem)
        {
            context.InvItems.Add(invItem);
            context.SaveChanges();
        }

        public void AddCartItem(CartItem cartItem)
        {
            context.CartItems.Add(cartItem);
            context.SaveChanges();
        }

        public void AddToInvItemQuantity(int locationId, int productId, int quantityAdded)
        {
            context.InvItems.Single(x => x.LocationId==locationId && x.ProductId==productId).Quantity += quantityAdded;
            context.SaveChanges();
        }

        public void EmptyCart(int cartId)
        {
            context.Carts.Include("Items").Single(x => x.Id==cartId).Items.Clear();
            context.SaveChanges();
        }

        public Cart GetCart(int customerId, int locationId)
        {
            return context.Carts.Single(x => x.CustomerId == customerId && x.LocationId == locationId);
        }

        public CartItem GetCartItem(int cartId, int productId)
        {
            return context.CartItems.Single(x => x.CartId==cartId && x.ProductId==productId);
        }

        public List<CartItem> GetCartItems(int cartId)
        {
            return context.CartItems.Include("Product").Where(x => x.CartId==cartId).ToList();
        }

        //TODO: Remove business logic
        public Customer GetDefaultCustomer()
        {
            if (!context.Customers.Any(x => true))
            {
                Customer customer = new Customer();
                customer.UserName = "Andres";
                customer.Address = new Address("123 Main Street", "Charles Town", "WV", 12345, "USA");
                context.Customers.Add(customer);
                context.SaveChanges();
            }
            return context.Customers.First(x => true);
        }

        public List<InvItem> GetInventory(int locationId)
        {
            return context.InvItems.Include("Product").Where(x => x.LocationId==locationId).ToList();
        }

        public List<Location> GetLocations()
        {
            return context.Locations.Select(x => x).ToList();
        }

        public List<Order> GetOrdersAscend(Func<Order, bool> where, Func<Order, Object> orderBy)
        {
            return context.Orders.Include("CustomerAddress")
                                 .Where(where)
                                 .OrderBy(orderBy)
                                 .ToList();
        }

        public List<Order> GetOrdersDescend(Func<Order, bool> where, Func<Order, Object> orderBy)
        {
            return context.Orders.Include("CustomerAddress")
                                 .Where(where)
                                 .OrderByDescending(orderBy)
                                 .ToList();
        }

        public List<OrderItem> GetOrderItems(int orderId)
        {
            return context.OrderItems.Include("Product").Where(x => x.OrderId==orderId).ToList();
        }

        public bool HasCart(int customerId, int locationId)
        {
            return context.Carts.Any(x => x.CustomerId==customerId && x.LocationId==locationId);
        }

        public bool HasCartItem(CartItem item)
        {
            return context.CartItems.Any(x => x.CartId==item.CartId && x.ProductId==item.ProductId);
        }

        public void PlaceOrderTransaction(int locationId, int cartId, Order order)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    foreach(CartItem item in GetCartItems(cartId))
                    {
                        ReduceInventory(locationId, item.ProductId, item.Quantity);
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

        public void RemoveCartItem(CartItem item)
        {
            context.CartItems.Remove(item);
            context.SaveChanges();
        }

        public void ReduceInventory(int locationId, int productId, int quantity)
        {
            context.InvItems.Single(x => x.LocationId==locationId && x.Product.Id==productId).Quantity -= quantity;
            context.SaveChanges();
        }

        public void UpdateCartItemQuantity(CartItem item)
        {
            context.CartItems.Single(x => x.CartId==item.CartId && x.ProductId==item.ProductId).Quantity = item.Quantity;
            context.SaveChanges();
        }
    }
}
using System;
using System.Collections.Generic;
using StoreDB.Models;

namespace StoreDB
{
    
    public interface ILocationRepo
    {
        List<InvItem> GetInventory(int locationId);
        void ReduceInventory(int locationId, int productId, int quantity);
        void AddInvItem(InvItem invItem);
        void AddToInvItemQuantity(int locationId, int productId, int quantityAdded);
        List<Order> GetOrdersAscend(Func<Order, bool> where, Func<Order, Object> orderBy);
        List<Order> GetOrdersDescend(Func<Order, bool> where, Func<Order, Object> orderBy);
        List<OrderItem> GetOrderItems(int orderId);
    }
}
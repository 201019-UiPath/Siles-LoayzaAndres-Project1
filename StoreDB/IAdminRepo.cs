using System;
using System.Collections.Generic;
using StoreDB.Models;

namespace StoreDB
{
    public interface IAdminRepo
    {
        void AddLocation(Location location);
        void AddInvItem(InvItem invItem);
        List<Order> GetOrdersAscend(Func<Order, bool> where, Func<Order, Object> orderBy);
        List<Order> GetOrdersDescend(Func<Order, bool> where, Func<Order, Object> orderBy);
        List<OrderItem> GetOrderItems(int orderId);
        void AddToInvItemQuantity(int locationId, int productId, int addend);
    }
}
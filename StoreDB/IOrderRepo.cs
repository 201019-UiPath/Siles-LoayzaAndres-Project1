using System;
using System.Collections.Generic;
using System.Text;
using StoreDB.Models;

namespace StoreDB
{
    public interface IOrderRepo
    {
        List<Order> GetOrdersAscend(Func<Order, bool> where, Func<Order, Object> orderBy);
        List<Order> GetOrdersDescend(Func<Order, bool> where, Func<Order, Object> orderBy);
        List<OrderItem> GetOrderItems(int orderId);
    }
}

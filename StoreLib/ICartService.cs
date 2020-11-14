using System.Collections.Generic;
using StoreDB.Models;

namespace StoreLib
{
    public interface ICartService
    {
        void AddToCart(CartItem item);
        void EmptyCart(int cartId);
        Cart GetCart(int customerId, int locationId);
        List<CartItem> GetCartItems(int cartId);
        decimal GetCost(int cartId);
        void PlaceOrder(Order order);
        Order PlaceOrder(int customerId, int locationId, Address returnAdd, Address destAdd);
        void RemoveCartItem(CartItem cartItem);
    }
}
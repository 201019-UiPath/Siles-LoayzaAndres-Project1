using System.Collections.Generic;
using StoreDB.Models;

namespace StoreLib
{
    public interface ICartService
    {
        void AddToCart(CartItem item);
        void EmptyCart();
        List<CartItem> GetCartItems();
        Order PlaceOrder();
        void RemoveCartItem(CartItem cartItem);
    }
}
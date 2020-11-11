using System.Collections.Generic;
using StoreDB.Models;

namespace StoreLib
{
    public interface ICartService
    {
        Cart Cart { get; }
        Customer Customer { get; }
        Location Location { get; }

        void AddToCart(CartItem item);
        void EmptyCart();
        List<CartItem> GetCartItems();
        Order PlaceOrder();
        void RemoveCartItem(CartItem cartItem);
        void WriteCart();
    }
}
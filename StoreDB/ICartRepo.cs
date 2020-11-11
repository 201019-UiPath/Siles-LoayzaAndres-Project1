using System.Collections.Generic;
using StoreDB.Models;

namespace StoreDB
{
    public interface ICartRepo
    {
        void AddCart(Cart cart);
        void AddCartItem(CartItem item);
        void RemoveCartItem(CartItem item);
        void EmptyCart(int cartId);
        Cart GetCart(int customerId, int locationId);
        CartItem GetCartItem(int cartId, int productId);
        List<CartItem> GetCartItems(int cartId);
        bool HasCart(int customerId, int locationId);
        bool HasCartItem(CartItem item);
        void PlaceOrderTransaction(int locationId, int cartId, Order order);
        void UpdateCartItemQuantity(CartItem item);
    }
}
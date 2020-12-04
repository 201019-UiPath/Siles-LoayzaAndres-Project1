using System.Collections.Generic;
using StoreDB.Models;

namespace StoreDB
{
    public interface ICartRepo
    {
        /// <summary>
        /// Adds the specified cart to the database and saves changes.
        /// </summary>
        /// <param name="cart"></param>
        void AddCart(Cart cart);
        /// <summary>
        /// Adds the specified cart item to the database and saves changes.
        /// </summary>
        /// <param name="cartItem"></param>
        void AddCartItem(CartItem item);
        /// <summary>
        /// Removes the specified cart item from the database. Saves changes.
        /// </summary>
        /// <param name="item"></param>
        void RemoveCartItem(CartItem item);
        /// <summary>
        /// Removes all items stored in the cart with the specified id from the
        /// database. Saves changes.
        /// </summary>
        /// <param name="cartId"></param>
        void EmptyCart(int cartId);
        /// <summary>
        /// Returns the cart stored in the database with the specified customer
        /// ID and location ID.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="locationId"></param>
        /// <returns>a cart</returns>
        Cart GetCart(int customerId, int locationId);
        /// <summary>
        /// Returns the cart stored in the database with the specified cart ID.
        /// </summary>
        /// <param name="cartId"></param>
        /// <returns>a cart</returns>
        Cart GetCart(int cartId);
        /// <summary>
        /// Returns the cart item stored in the database with the specified 
        /// cart ID and product ID.
        /// </summary>
        /// <param name="cartId"></param>
        /// <param name="productId"></param>
        /// <returns>a cart item</returns>
        CartItem GetCartItem(int cartId, int productId);
        /// <summary>
        /// Returns all cart items stored in the database with the specified
        /// cart ID.
        /// </summary>
        /// <param name="cartId"></param>
        /// <returns>a list of cart items</returns>
        List<CartItem> GetCartItems(int cartId);
        /// <summary>
        /// Returns true if the database has a cart with the specified customer
        /// ID and location ID. Returns false otherwise.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="locationId"></param>
        /// <returns>a bool</returns>
        bool HasCart(int customerId, int locationId);
        /// <summary>
        /// Returns true if the database has a cart item with the cart ID and
        /// the product ID of the specified cart item. Returns false otherwise.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>a bool</returns>
        bool HasCartItem(CartItem item);
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
        void PlaceOrderTransaction(int locationId, int cartId, Order order);
    }
}
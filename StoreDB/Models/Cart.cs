using System;
using System.Collections.Generic;

namespace StoreDB.Models
{
    /// <summary>
    /// Represents a unique cart for a particular customer at a particular
    /// location. Contains a List of CartItems. Calculates cost based on
    /// the sum of prices in that List.
    /// </summary>
    public class Cart
    {
        /// <summary>
        /// Unique ID. Primary key in the database.
        /// </summary>
        /// <value></value>
        public int Id {get; set;}
        /// <summary>
        /// ID for the Location associated with this Cart.
        /// </summary>
        /// <value></value>
        public int LocationId {get; set;}
        /// <summary>
        /// ID for the Customer associated with this Cart.
        /// </summary>
        /// <value></value>
        public int CustomerId {get; set;}
        /// <summary>
        /// List of products contained in this Cart.
        /// </summary>
        /// <value></value>
        public List<CartItem> Items {get; set;}
        /// <summary>
        /// Number of unique products in this Cart. Does not count quantity.
        /// </summary>
        /// <value></value>
        public int Count { get{return Items.Count;} }
        /// <summary>
        /// Total cost of all items in this Cart. Calculated in the getter.
        /// </summary>
        /// <value></value>
        public decimal Cost
        {
            get
            {
                decimal total = 0;
                foreach(var invItem in Items)
                {
                    total += (invItem.Quantity * invItem.Product.Price);
                }
                return total;
            }
        }

        public Cart()
        {
            Items = new List<CartItem>();
        }
    }
}
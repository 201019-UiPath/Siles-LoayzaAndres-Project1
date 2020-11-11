using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreDB.Models
{
    /// <summary>
    /// Represents a unique cart for a particular customer at a particular
    /// location. Contains a List of CartItems. Calculates cost based on
    /// the sum of prices in that List.
    /// </summary>
    public class Cart
    {
        [Key]
        public int Id {get; set;}
        /// <summary>
        /// ID for the Location associated with this Cart.
        /// </summary>
        [ForeignKey("LocationId")]
        public int LocationId {get; set;}
        public Location Location {get; set;}
        /// <summary>
        /// ID for the Customer associated with this Cart.
        /// </summary>
        [ForeignKey("CustomerId")]
        public int CustomerId {get; set;}
        public Customer Customer {get; set;}
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
                foreach(var item in Items)
                {
                    total += (item.Quantity * item.Price);
                }
                return total;
            }
        }
    }
}
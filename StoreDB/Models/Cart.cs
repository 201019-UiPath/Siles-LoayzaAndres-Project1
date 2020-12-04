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
        /// <summary>
        /// ID for this Cart.
        /// </summary>
        /// <value></value>
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
    }
}
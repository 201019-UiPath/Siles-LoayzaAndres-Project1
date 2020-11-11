using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StoreDB.Models
{
    /// <summary>
    /// Represents a Customer shopping at the store, including unique 
    /// identifiers, shipping address, and order history for all store 
    /// locations.
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Unique ID. Primary key in the database.
        /// </summary>
        [Key]
        public int Id {get; set;}

        /// <summary>
        /// Username for this Customer. Should be unique.
        /// </summary>
        [Required]
        public string UserName {get; set;}

        [Required]
        public string Password { get; set; }

        /// <summary>
        /// Shipping address for this Customer.
        /// </summary>
        public Address Address {get; set;}

        /// <summary>
        /// List of Orders for this Customer. Represents this Customer's
        /// order history.
        /// </summary>
        /// <value></value>
        public List<Order> Orders {get; set;}
    }
}
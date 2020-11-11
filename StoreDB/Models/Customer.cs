using System.Collections.Generic;

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
        /// <value></value>
        public int Id {get; set;}
        /// <summary>
        /// Username for this Customer. Should be unique.
        /// </summary>
        /// <value></value>
        public string UserName {get; set;}
        /// <summary>
        /// Shipping address for this Customer.
        /// </summary>
        /// <value></value>
        public Address Address {get; set;}
        /// <summary>
        /// List of Orders for this Customer. Represents this Customer's
        /// order history.
        /// </summary>
        /// <value></value>
        public List<Order> Orders {get; set;}

        public Customer()
        {
            Orders = new List<Order>();
        }
    }
}
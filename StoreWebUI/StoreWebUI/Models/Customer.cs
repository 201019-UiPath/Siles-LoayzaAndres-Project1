using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StoreWebUI.Models
{
    /// <summary>
    /// Represents a Customer shopping at the store, including unique 
    /// identifiers, shipping address, and order history for all store 
    /// locations.
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Username for this Customer. Should be unique.
        /// </summary>
        [Required]
        [DisplayName("Username")]
        [DataType(DataType.Text)]
        public string UserName {get; set;}

        [Required]
        [DataType(DataType.Password)]
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
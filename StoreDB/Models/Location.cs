using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StoreDB.Models
{
    /// <summary>
    /// Represents a store location, including address, inventory, and order
    /// history.
    /// </summary>
    public class Location
    {
        /// <summary>
        /// Unique ID. Primary key in the database.
        /// </summary>
        [Key]
        public int Id {get; set;}

        /// <summary>
        /// Name of location used in UI.
        /// </summary>
        [Required]
        public string Name {get; set;}

        /// <summary>
        /// Address for this Location.
        /// </summary>
        public Address Address {get; set;}

        /// <summary>
        /// List of all available products at this store Location.
        /// </summary>
        public List<InvItem> Inventory {get; set;}
        /// <summary>
        /// List of all orders processed at this store Location.
        /// </summary>
        public List<Order> Orders {get; set;}

    }
}
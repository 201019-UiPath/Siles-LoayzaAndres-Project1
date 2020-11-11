using System.Collections.Generic;

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
        /// <value></value>
        public int Id {get; set;}

        /// <summary>
        /// Name of location used in UI.
        /// </summary>
        public string Name {get; set;}

        /// <summary>
        /// Address for this Location.
        /// </summary>
        public Address Address {get; set;}

        /// <summary>
        /// List of all available products at this store Location.
        /// </summary>
        /// <value></value>
        public List<InvItem> Inventory {get; set;}
        /// <summary>
        /// List of all orders processed at this store Location.
        /// </summary>
        /// <value></value>
        public List<Order> Orders {get; set;}

        public Location()
        {
            Inventory = new List<InvItem>();
            Orders = new List<Order>();
        }

    }
}
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreDB.Models
{
    /// <summary>
    /// Represents a product and its quantity at a store Location.
    /// </summary>
    public class InvItem : Item
    {
        /// <summary>
        /// ID for the Location associated with this InvItem.
        /// </summary>
        [ForeignKey("LocationId")]
        public int LocationId {get; set;}
        public Location Location {get; set;}
    }
}
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreWebUI.Models
{
    /// <summary>
    /// Represents a product and its quantity in an Order.
    /// </summary>
    public class OrderItem : Item
    {
        /// <summary>
        /// ID for the Order associated with this OrderItem.
        /// </summary>
        [ForeignKey("OrderId")]
        public int OrderId {get; set;}
        public Order Order {get; set;}
    }
}
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreWebUI.Models
{
    /// <summary>
    /// Represents a product and its quantity inside a Cart.
    /// </summary>
    public class CartItem : Item
    {
        /// <summary>
        /// ID for the Cart associated with this CartItem.
        /// </summary>
        [ForeignKey("CartId")]
        public int CartId {get; set;}
        public Cart Cart {get; set;}
    }
}
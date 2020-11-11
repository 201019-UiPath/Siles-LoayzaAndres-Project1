
namespace StoreDB.Models
{
    /// <summary>
    /// Represents a product and its quantity inside a Cart.
    /// </summary>
    public class CartItem : Item
    {
        /// <summary>
        /// ID for the Cart associated with this CartItem.
        /// </summary>
        /// <value></value>
        public int CartId {get; set;}
        public CartItem() {}

        public CartItem(Product product, int quantity)
        {
            this.ProductId = product.Id;
            this.Product = product;
            this.Quantity = quantity;
        }
    }
}
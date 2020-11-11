namespace StoreDB.Models
{
    /// <summary>
    /// Represents a product and its quantity in an Order.
    /// </summary>
    public class OrderItem : Item
    {
        /// <summary>
        /// ID for the Order associated with this OrderItem.
        /// </summary>
        /// <value></value>
        public int OrderId {get; set;}

        public OrderItem() {}

        public OrderItem(Product product, int quantity)
        {
            this.ProductId = product.Id;
            this.Product = product;
            this.Quantity = quantity;
        }
    }
}
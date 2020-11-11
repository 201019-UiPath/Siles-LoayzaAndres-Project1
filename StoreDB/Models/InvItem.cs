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
        /// <value></value>
        public int LocationId {get; set;}

        public InvItem() {}
        
        public InvItem(Product product, int quantity)
        {
            ProductId = product.Id;
            Product = product;
            Quantity = quantity;
        }
    }
}
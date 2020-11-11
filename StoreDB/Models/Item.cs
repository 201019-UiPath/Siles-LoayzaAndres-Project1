namespace StoreDB.Models
{
    /// <summary>
    /// Represents a Product and a quantity for that Product. This is an 
    /// abstract class meant to provide a base for different kinds of items
    /// placed in different states.
    /// </summary>
    public abstract class Item
    {
        /// <summary>
        /// ID for the Product associated with this Item.
        /// </summary>
        /// <value></value>
        public int ProductId {get; set;}
        /// <summary>
        /// The Product associated with this Item.
        /// </summary>
        /// <value></value>
        public Product Product {get; set;}
        /// <summary>
        /// The amount of the Product contained in this Item.
        /// </summary>
        /// <value></value>
        public int Quantity {get; set;}
        
        public void Write()
        {
            Product.Write();
            System.Console.WriteLine($"    Quantity: {Quantity}");
        }
    }
}
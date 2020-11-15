using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreWebUI.Models
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
        [ForeignKey("ProductId")]
        public int ProductId {get; set;}
        /// <summary>
        /// The Product associated with this Item.
        /// </summary>
        /// <value></value>
        [Required]
        public Product Product {get; set;}
        /// <summary>
        /// Price in USD for this Product.
        /// </summary>
        private decimal price;
        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get { return price; } set { price = decimal.Round(value, 2); } }
        /// <summary>
        /// The amount of the Product contained in this Item.
        /// </summary>
        /// <value></value>
        [Required]
        public int Quantity {get; set;}
    }
}
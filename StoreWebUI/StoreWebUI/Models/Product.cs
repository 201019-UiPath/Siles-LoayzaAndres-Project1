using System;
using System.ComponentModel.DataAnnotations;

namespace StoreWebUI.Models
{
    /// <summary>
    /// Represents a unique product. Includes a name, description, and price.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// ID number for this Product.
        /// </summary>
        [Key]
        public int Id {get; set;}

        /// <summary>
        /// Short descriptive name for this Product.
        /// </summary>
        [Required]
        public string Name {get; set;}

        /// <summary>
        /// Description for this Product.
        /// </summary>
        public string Description {get; set;}

    }
}

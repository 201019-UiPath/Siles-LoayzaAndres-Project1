using System.ComponentModel.DataAnnotations;

namespace StoreWebUI.Models
{
    /// <summary>
    /// Represents a real-world mailing address.
    /// </summary>
    public class Address
    {
        /// <summary>
        /// Unique ID. Primary key in the database.
        /// </summary>
        [Key]
        public int Id {get; set;}
        /// <summary>
        /// Street address.
        /// </summary>
        [Required]
        [DataType(DataType.Text)]
        public string Street {get; set;}
        /// <summary>
        /// City.
        /// </summary>
        [Required]
        [DataType(DataType.Text)]
        public string City {get; set;}
        /// <summary>
        /// Zip code.
        /// </summary>
        /// <value></value>
        [DataType(DataType.PostalCode)]
        public int Zip {get; set;}
        /// <summary>
        /// State or territory.
        /// </summary>
        /// <value></value>
        public string State {get; set;}
        /// <summary>
        /// Country or nation.
        /// </summary>
        [Required]
        public string Country {get; set;}

        public override string ToString()
        {
            return $"{Street} {City}, {State} {Zip}, {Country}";
        }
    }
}
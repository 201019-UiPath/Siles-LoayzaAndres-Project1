using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreDB.Models
{
    /// <summary>
    /// Represents an order placed by a customer at a specific store location, 
    /// consisting of products, a timestamp, and a total cost.
    /// </summary>
    public class Order
    {
        public int Id {get; set;}
        /// <summary>
        /// ID for the Location associated with this Order.
        /// </summary>
        [ForeignKey("LocationId")]
        public int LocationId {get; set;}
        public Location Location {get; set;}
        /// <summary>
        /// ID for the Customer associated with this Order.
        /// </summary>
        [ForeignKey("CustomerId")]
        public int CustomerId {get; set;}
        public Customer Customer {get; set;}
        /// <summary>
        /// Address for the Location associated with this Order.
        /// </summary>
        public Address ReturnAddress {get; set;}
        /// <summary>
        /// Address for the Customer associated with this Order.
        /// </summary>
        public Address DestinationAddress {get; set;}
        /// <summary>
        /// Date and time that this Order was placed.
        /// </summary>
        public DateTime CreationTime {get; set;}
        /// <summary>
        /// List of products that this Order contains.
        /// </summary>
        public List<OrderItem> Items {get; set;}
        /// <summary>
        /// Total cost for this Order in USD.
        /// </summary>
        public decimal Cost {get; set;}
    }
}
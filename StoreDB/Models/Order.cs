using System;
using System.Collections.Generic;

namespace StoreDB.Models
{
    /// <summary>
    /// Represents an order placed by a customer at a specific store location, 
    /// consisting of products, a timestamp, and a total cost.
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Unique ID. Primary key for the database.
        /// </summary>
        /// <value></value>
        public int Id {get; set;}
        /// <summary>
        /// ID for the Location associated with this Order.
        /// </summary>
        /// <value></value>
        public int LocationId {get; set;}
        /// <summary>
        /// ID for the Customer associated with this Order.
        /// </summary>
        /// <value></value>
        public int CustomerId {get; set;}
        /// <summary>
        /// Address for the Location associated with this Order.
        /// </summary>
        /// <value></value>
        public Address LocationAddress {get; set;}
        /// <summary>
        /// Address for the Customer associated with this Order.
        /// </summary>
        /// <value></value>
        public Address CustomerAddress {get; set;}
        /// <summary>
        /// Date and time that this Order was placed.
        /// </summary>
        /// <value></value>
        public DateTime DateTime {get; set;}
        /// <summary>
        /// List of products that this Order contains.
        /// </summary>
        /// <value></value>
        public List<OrderItem> Items {get; set;}
        /// <summary>
        /// Total cost for this Order in USD.
        /// </summary>
        /// <value></value>
        public decimal Cost {get; set;}

        public Order()
        {
            Items = new List<OrderItem>();
        }

        public void Write()
        {
            Console.WriteLine($"Order ID: {Id}");
            Console.WriteLine($"Date: {DateTime.ToShortDateString()}");
            Console.WriteLine($"Time: {DateTime.ToShortTimeString()}");
            foreach(OrderItem item in Items)
            {
                Console.WriteLine($"    {item.Quantity} of {item.Product.Name}");
            }
            Console.WriteLine($"    Total Cost: ${Cost}");
        }
    }
}
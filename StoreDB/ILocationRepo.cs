using System;
using System.Collections.Generic;
using StoreDB.Models;

namespace StoreDB
{
    
    public interface ILocationRepo
    {
        /// <summary>
        /// Returns all location stored in the database.
        /// </summary>
        /// <returns>a list of locations</returns>
        List<Location> GetLocations();
        /// <summary>
        /// Returns the inventory item stored in the database with the 
        /// specified location ID and product ID.
        /// </summary>
        /// <param name="locationId"></param>
        /// <param name="productId"></param>
        /// <returns>an inventory item</returns>
        InvItem GetInvItem(int locationId, int productId);
        /// <summary>
        /// Returns all inventory items stored in the database with the 
        /// specified location ID.
        /// </summary>
        /// <param name="locationId"></param>
        /// <returns>a list of inventory items</returns>
        List<InvItem> GetInvItemsByLocation(int locationId);
    }
}
using System;
using System.Collections.Generic;
using StoreDB.Models;

namespace StoreDB
{
    
    public interface ILocationRepo
    {
        List<Location> GetLocations();
        InvItem GetInvItem(int locationId, int productId);
        List<InvItem> GetInvItemsByLocation(int locationId);
    }
}
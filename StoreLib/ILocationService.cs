using System.Collections.Generic;
using StoreDB.Models;

namespace StoreLib
{
    public interface ILocationService
    {
        List<Location> GetLocations();
        List<InvItem> GetInventory(int locationId);
        InvItem GetInvItem(int locationId, int productId);
        bool LocationHasItem(int locationId, int productId);
    }
}
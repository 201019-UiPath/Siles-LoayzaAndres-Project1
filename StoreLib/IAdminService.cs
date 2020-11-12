using System.Collections.Generic;
using StoreDB.Models;

namespace StoreLib
{
    public interface IAdminService : ILocationService
    {
        void AddLocation(Location location);
        void AddNewProductToInventory(int locationId, InvItem invItem);
        void AddToInvItem(int locationId, int productId, int addend);
        List<Order> GetOrdersByCostAscend(int locationId);
        List<Order> GetOrdersByCostDescend(int locationId);
        List<Order> GetOrdersByDateAscend(int locationId);
        List<Order> GetOrdersByDateDescend(int locationId);
    }
}
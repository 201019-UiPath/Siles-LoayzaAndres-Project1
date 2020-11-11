using System.Collections.Generic;
using StoreDB.Models;

namespace StoreLib
{
    public interface IItemService
    {
        void AddNewProductToInventory(int locationId, InvItem invItem);
        void AddToInvItem(int locationId, int productId, int addend);
        List<InvItem> GetInventory(int locationId);
        InvItem GetInvItem(int locationId, int productId);
        bool LocationHasItem(int locationId, int productId);
    }
}
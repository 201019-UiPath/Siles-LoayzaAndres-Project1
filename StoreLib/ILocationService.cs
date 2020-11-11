using System.Collections.Generic;
using StoreDB.Models;

namespace StoreLib
{
    public interface ILocationService
    {
        Location Location { get; }

        void AddNewProductToInventory(InvItem invItem);
        void AddToInvItem(int productId, int quantityAdded);
        List<InvItem> GetInventory();
        bool HasProduct(int productId);
        void WriteInventory();
        void WriteOrdersByCostAscend();
        void WriteOrdersByCostDescend();
        void WriteOrdersByDateAscend();
        void WriteOrdersByDateDescend();
    }
}
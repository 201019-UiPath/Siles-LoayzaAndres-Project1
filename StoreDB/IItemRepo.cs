using System.Collections.Generic;
using StoreDB.Models;

namespace StoreDB
{
    public interface IItemRepo
    {
        void AddInvItem(InvItem invItem);
        InvItem GetInvItem(int locationId, int productId);
        List<InvItem> GetInvItemsByLocation(int locationId);
    }
}

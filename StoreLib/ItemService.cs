using System;
using System.Collections.Generic;
using System.Text;
using StoreDB;
using StoreDB.Models;

namespace StoreLib
{
    public class ItemService : IItemService
    {
        IItemRepo repo;

        public ItemService(IItemRepo repo)
        {
            this.repo = repo;
        }

        public void AddNewProductToInventory(int locationId, InvItem invItem)
        {
            invItem.LocationId = locationId;
            if (!LocationHasItem(locationId, invItem.ProductId))
            {
                repo.AddInvItem(invItem);
            }
            else
            {
                System.Console.WriteLine("Error! Product already exists!");
            }
        }

        public void AddToInvItem(int locationId, int productId, int addend)
        {
            GetInvItem(locationId, productId).Quantity += addend;
        }

        public InvItem GetInvItem(int locationId, int productId)
        {
            return repo.GetInvItem(locationId, productId);
        }

        public List<InvItem> GetInventory(int locationId)
        {
            return repo.GetInvItemsByLocation(locationId);
        }

        public bool LocationHasItem(int locationId, int productId)
        {
            List<InvItem> inventory = repo.GetInvItemsByLocation(locationId);
            foreach (var invItem in inventory)
            {
                if (invItem.Product.Id == productId)
                {
                    return true;
                }
            }
            return false;
        }
    }
}

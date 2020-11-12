using System;
using System.Collections.Generic;
using StoreDB;
using StoreDB.Models;

namespace StoreLib
{
    /// <summary>
    /// Provides service for a specific location, based on the LocationRepo
    /// given in the constructor. The business logic for accessing a location's
    /// inventory and order history is contained here.
    /// </summary>
    public class LocationService : ILocationService
    {
        private ILocationRepo repo;

        public LocationService(ILocationRepo repo)
        {
            this.repo = repo;
        }

        public List<Location> GetLocations()
        {
            return repo.GetLocations();
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
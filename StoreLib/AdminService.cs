using System.Collections.Generic;
using Serilog;
using StoreDB;
using StoreDB.Models;

namespace StoreLib
{
    public class AdminService : LocationService, IAdminService
    {
        IAdminRepo repo;

        public AdminService(IAdminRepo repo) : base((ILocationRepo)repo)
        {
            this.repo = repo;
        }

        public void AddLocation(Location location)
        {
            repo.AddLocation(location);
            Log.Information($"Created new location {location.Name}");
        }

        public void AddNewProductToInventory(int locationId, InvItem invItem)
        {
            invItem.LocationId = locationId;
            if (!LocationHasItem(locationId, invItem.ProductId))
            {
                repo.AddInvItem(invItem);
                Log.Information($"Added new product {invItem.ProductId} to location {locationId}.");
            }
            else
            {
                Log.Warning($"Failed to add new product {invItem.ProductId} to location {locationId}.");
            }
        }

        public void AddToInvItem(int locationId, int productId, int addend)
        {
            repo.AddToInvItemQuantity(locationId, productId, addend);
            Log.Information($"Added quantity {addend} to product {productId} at location {locationId}.");
        }

        public List<Order> GetOrdersByDateAscend(int locationId)
        {
            return repo.GetOrdersAscend((x => x.LocationId == locationId), (x => x.CreationTime));
        }

        public List<Order> GetOrdersByDateDescend(int locationId)
        {
            return repo.GetOrdersDescend((x => x.LocationId == locationId), (x => x.CreationTime));
        }

        public List<Order> GetOrdersByCostAscend(int locationId)
        {
            return repo.GetOrdersAscend((x => x.LocationId == locationId), (x => x.Cost));
        }

        public List<Order> GetOrdersByCostDescend(int locationId)
        {
            return repo.GetOrdersDescend((x => x.LocationId == locationId), (x => x.Cost));
        }
    }
}
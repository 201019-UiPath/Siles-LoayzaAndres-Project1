using System.Collections.Generic;
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
            repo.AddToInvItemQuantity(locationId, productId, addend);
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
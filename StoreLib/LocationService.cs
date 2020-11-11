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
        public Location Location { get; private set; }

        public LocationService(ILocationRepo repo, Location location)
        {
            this.repo = repo;
            this.Location = location;
        }

        public void AddNewProductToInventory(InvItem invItem)
        {
            invItem.LocationId = Location.Id;
            if (!HasProduct(invItem.ProductId))
            {
                repo.AddInvItem(invItem);
            }
            else
            {
                System.Console.WriteLine("Error! Product already exists!");
            }
        }

        public List<InvItem> GetInventory()
        {
            return repo.GetInventory(Location.Id);
        }

        public void WriteInventory()
        {
            List<InvItem> inventory = GetInventory();
            int i = 0;
            foreach (var item in inventory)
            {
                Console.Write($"[{i}] ");
                item.Write();
                i++;
            }
        }

        /// <summary>
        /// Returns true if the given Product exists already in this Location's
        /// Inventory. Iterates through Inventory and compares the Product in
        /// each InvItem with the given Product using the Equals method.
        /// </summary>
        /// <param name="product"></param>
        /// <returns>true if given Product is in this Inventory</returns>
        public bool HasProduct(int productId)
        {
            List<InvItem> inventory = repo.GetInventory(Location.Id);
            foreach (var invItem in inventory)
            {
                if (invItem.Product.Id == productId)
                {
                    return true;
                }
            }
            return false;
        }

        public void AddToInvItem(int productId, int quantityAdded)
        {
            repo.AddToInvItemQuantity(Location.Id, productId, quantityAdded);
        }

        private void WriteOrders(List<Order> orders)
        {
            foreach (Order o in orders)
            {
                o.Items = repo.GetOrderItems(o.Id);
                o.Write();
            }
            Console.WriteLine($"{orders.Count} orders found.");
        }

        public void WriteOrdersByDateAscend()
        {
            List<Order> orders = repo.GetOrdersAscend((x => x.LocationId == Location.Id), (x => x.DateTime));
            WriteOrders(orders);
        }

        public void WriteOrdersByDateDescend()
        {
            List<Order> orders = repo.GetOrdersDescend((x => x.LocationId == Location.Id), (x => x.DateTime));
            WriteOrders(orders);
        }

        public void WriteOrdersByCostAscend()
        {
            List<Order> orders = repo.GetOrdersAscend((x => x.LocationId == Location.Id), (x => x.Cost));
            WriteOrders(orders);
        }

        public void WriteOrdersByCostDescend()
        {
            List<Order> orders = repo.GetOrdersDescend((x => x.LocationId == Location.Id), (x => x.Cost));
            WriteOrders(orders);
        }
    }
}
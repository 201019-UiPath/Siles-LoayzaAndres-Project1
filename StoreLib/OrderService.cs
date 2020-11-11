using System;
using System.Collections.Generic;
using StoreDB;
using StoreDB.Models;

namespace StoreLib
{
    class OrderService : IOrderService
    {
        private IOrderRepo repo;

        public OrderService(IOrderRepo repo)
        {
            this.repo = repo;
        }

        public List<Order> GetCustomerOrdersByDateAscend(int customerId)
        {
            return repo.GetOrdersAscend((x => x.CustomerId == customerId), (x => x.CreationTime));
        }

        public List<Order> GetCustomerOrdersByDateDescend(int customerId)
        {
            return repo.GetOrdersDescend((x => x.CustomerId == customerId), (x => x.CreationTime));
        }

        public List<Order> GetCustomerOrdersByCostAscend(int customerId)
        {
            return repo.GetOrdersAscend((x => x.CustomerId == customerId), (x => x.Cost));
        }

        public List<Order> GetCustomerOrdersByCostDescend(int customerId)
        {
            return repo.GetOrdersDescend((x => x.CustomerId == customerId), (x => x.Cost));
        }

        public List<Order> GetLocationOrdersByDateAscend(int locationId)
        {
            return repo.GetOrdersAscend((x => x.LocationId == locationId), (x => x.CreationTime));
        }

        public List<Order> GetLocationOrdersByDateDescend(int locationId)
        {
            return repo.GetOrdersDescend((x => x.LocationId == locationId), (x => x.CreationTime));
        }

        public List<Order> GetLocationOrdersByCostAscend(int locationId)
        {
            return repo.GetOrdersAscend((x => x.LocationId == locationId), (x => x.Cost));
        }

        public List<Order> GetLocationOrdersByCostDescend(int locationId)
        {
            return repo.GetOrdersDescend((x => x.LocationId == locationId), (x => x.Cost));
        }
    }
}

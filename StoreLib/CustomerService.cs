using System;
using System.Collections.Generic;
using StoreDB;
using StoreDB.Models;

namespace StoreLib
{
    public class CustomerService : ICustomerService
    {
        private ICustomerRepo repo;
        public Customer Customer { get; private set; }

        public CustomerService(ICustomerRepo repo, Customer customer)
        {
            this.repo = repo;
            this.Customer = customer;
        }

        public List<Location> GetLocations()
        {
            return repo.GetLocations();
        }

        public void WriteOrdersByDateAscend()
        {
            List<Order> orders = repo.GetOrdersAscend((x => x.CustomerId == Customer.Id), (x => x.DateTime));
            WriteOrders(orders);
        }

        public void WriteOrdersByDateDescend()
        {
            List<Order> orders = repo.GetOrdersDescend((x => x.CustomerId == Customer.Id), (x => x.DateTime));
            WriteOrders(orders);
        }

        public void WriteOrdersByCostAscend()
        {
            List<Order> orders = repo.GetOrdersAscend((x => x.CustomerId == Customer.Id), (x => x.Cost));
            WriteOrders(orders);
        }

        public void WriteOrdersByCostDescend()
        {
            List<Order> orders = repo.GetOrdersDescend((x => x.CustomerId == Customer.Id), (x => x.Cost));
            WriteOrders(orders);
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
    }
}
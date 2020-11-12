using System;
using System.Collections.Generic;
using Serilog;
using StoreDB;
using StoreDB.Models;

namespace StoreLib
{
    public class CustomerService : LocationService, ICustomerService
    {
        private ICustomerRepo repo;

        public CustomerService(ICustomerRepo repo) : base((ILocationRepo)repo)
        {
            this.repo = repo;
        }

        public void SignUpNewCustomer(Customer customer)
        {
            if (repo.HasCustomer(customer.UserName))
            {
                Log.Warning($"Failed to sign up new customer: {customer.UserName}. Username already exists.");
                throw new ArgumentException("Could not sign up new customer. Username already exists.");
            }
            repo.AddCustomer(customer);
            Log.Information($"Added new customer: {customer.UserName}");
        }

        public Customer SignInExistingCustomer(string username, string password)
        {
            if (!repo.HasCustomer(username))
            {
                Log.Warning($"Failed to sign in customer: {username}. Username not found.");
                throw new ArgumentException("Could not sign in. Username not found.");
            }
            Customer customer = repo.GetCustomer(username);
            if (customer.Password!=password)
            {
                Log.Warning($"Failed to sign in customer: {username}. Password mismatch.");
                throw new UnauthorizedAccessException("Could not sign in. Password invalid.");
            }
            Log.Information($"Signed in customer: {username}.");
            return customer;
        }

        public List<Order> GetOrdersByDateAscend(int customerId)
        {
            return repo.GetOrdersAscend((x => x.CustomerId == customerId), (x => x.CreationTime));
        }

        public List<Order> GetOrdersByDateDescend(int customerId)
        {
            return repo.GetOrdersDescend((x => x.CustomerId == customerId), (x => x.CreationTime));
        }

        public List<Order> GetOrdersByCostAscend(int customerId)
        {
            return repo.GetOrdersAscend((x => x.CustomerId == customerId), (x => x.Cost));
        }

        public List<Order> GetOrdersByCostDescend(int customerId)
        {
            return repo.GetOrdersDescend((x => x.CustomerId == customerId), (x => x.Cost));
        }
    }
}
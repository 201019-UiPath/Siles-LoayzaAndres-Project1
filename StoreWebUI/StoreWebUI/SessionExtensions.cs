using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using StoreWebUI.Models;

namespace StoreWebUI
{
    public static class SessionExtensions
    {
        public const string SessionKeyCustomer = "CurrentCustomer";
        public const string SessionKeyLocation = "CurrentLocation";
        public const string SessionKeyCart = "CurrentCart";

        public static bool HasCustomer(this ISession session)
        {
            return session.IsAvailable && session.Get(SessionKeyCustomer) != null;
        }

        public static void SetCustomer(this ISession session, Customer customer)
        {
            session.SetString(SessionKeyCustomer, JsonSerializer.Serialize(customer));
        }

        public static Customer GetCustomer(this ISession session)
        {
            return JsonSerializer.Deserialize<Customer>(session.Get(SessionKeyCustomer));
        }

        public static void RemoveCustomer(this ISession session)
        {
            session.Remove(SessionKeyCustomer);
        }

        public static bool HasLocation(this ISession session)
        {
            return session.IsAvailable && session.Get(SessionKeyLocation) != null;
        }

        public static void SetLocation(this ISession session, Location location)
        {
            session.SetString(SessionKeyLocation, JsonSerializer.Serialize(location));
        }

        public static Location GetLocation(this ISession session)
        {
            return JsonSerializer.Deserialize<Location>(session.Get(SessionKeyLocation));
        }

        public static void RemoveLocation(this ISession session)
        {
            session.Remove(SessionKeyLocation);
        }

        public static bool HasCart(this ISession session)
        {
            return session.IsAvailable && session.Get(SessionKeyCart) != null;
        }

        public static void SetCart(this ISession session, Cart cart)
        {
            session.SetString(SessionKeyCart, JsonSerializer.Serialize(cart));
        }

        public static Cart GetCart(this ISession session)
        {
            return JsonSerializer.Deserialize<Cart>(session.Get(SessionKeyCart));
        }

        public static void RemoveCart(this ISession session)
        {
            session.Remove(SessionKeyCart);
        }
    }
}

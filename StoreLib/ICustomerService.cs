using System.Collections.Generic;
using StoreDB.Models;

namespace StoreLib
{
    public interface ICustomerService
    {
        Customer Customer { get; }

        List<Location> GetLocations();
        void WriteOrdersByCostAscend();
        void WriteOrdersByCostDescend();
        void WriteOrdersByDateAscend();
        void WriteOrdersByDateDescend();
    }
}
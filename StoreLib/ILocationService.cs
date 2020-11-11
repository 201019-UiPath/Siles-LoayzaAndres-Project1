using System.Collections.Generic;
using StoreDB.Models;

namespace StoreLib
{
    public interface ILocationService
    {
        void AddLocation(Location location);
        List<Location> GetLocations();
    }
}
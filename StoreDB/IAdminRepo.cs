using System.Collections.Generic;
using StoreDB.Models;

namespace StoreDB
{
    public interface IAdminRepo
    {
        void AddLocation(Location location);

        List<Location> GetLocations();
    }
}
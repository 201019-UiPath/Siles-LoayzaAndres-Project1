using System.Collections.Generic;
using StoreDB.Models;

namespace StoreLib
{
    public interface IAdminService
    {
        void AddLocation(Location location);
        List<Location> GetLocations();
    }
}
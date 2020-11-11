using System;
using System.Collections.Generic;
using StoreDB.Models;

namespace StoreDB
{
    
    public interface ILocationRepo
    {
        void AddLocation(Location location);
        List<Location> GetLocations();
    }
}
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

        public void AddLocation(Location location)
        {
            repo.AddLocation(location);
        }

        public List<Location> GetLocations()
        {
            return repo.GetLocations();
        }

    }
}
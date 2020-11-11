using System.Collections.Generic;
using StoreDB;
using StoreDB.Models;

namespace StoreLib
{
    public class AdminService : IAdminService
    {
        IAdminRepo repo;

        public AdminService(IAdminRepo repo)
        {
            this.repo = repo;
        }
    }
}
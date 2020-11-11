namespace StoreDB
{
    public interface IStoreRepo : ICustomerRepo, ILocationRepo, ICartRepo, IAdminRepo, IOrderRepo, IItemRepo
    {
         
    }
}
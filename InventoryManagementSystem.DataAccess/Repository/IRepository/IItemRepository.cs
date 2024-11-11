using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.DataAccess.Repository.IRepository
{
    public interface IItemRepository : IGenericRepository<TbItem>
    {
        void Update(TbItem entity);
    }
}

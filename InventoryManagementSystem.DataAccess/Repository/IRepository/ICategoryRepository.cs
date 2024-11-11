using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.DataAccess.Repository.IRepository
{
    public interface ICategoryRepository : IGenericRepository<TbCategory>
    {
        void Update(TbCategory entity);
    }
}

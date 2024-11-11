using InventoryManagementSystem.DataAccess.Data;
using InventoryManagementSystem.DataAccess.Repository.IRepository;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.DataAccess.Repository
{
    public class ItemRepository : GenericRepository<TbItem>, IItemRepository
    {
        private readonly AppDbContext _context;

        public ItemRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(TbItem entity)
        {
            _context.Items.Update(entity);
        }
    }
}


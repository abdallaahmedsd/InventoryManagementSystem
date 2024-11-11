using InventoryManagementSystem.DataAccess.Data;
using InventoryManagementSystem.DataAccess.Repository.IRepository;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.DataAccess.Repository
{
    public class CategoryRepository : GenericRepository<TbCategory>, ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(TbCategory entity)
        {
            _context.Categories.Update(entity);
        }
    }
}


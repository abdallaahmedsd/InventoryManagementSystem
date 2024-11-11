using InventoryManagementSystem.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.DataAccess.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly AppDbContext _dbContext;

        public DbInitializer(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Initialize()
        {
            try
            {
                // Apply any pending migrations to ensure the database is up-to-date
                if (_dbContext.Database.GetPendingMigrations().Any())
                {
                    _dbContext.Database.Migrate();
                }
            }
            catch (Exception ex)
            {
                // log the expception
            }
        }
    }
}

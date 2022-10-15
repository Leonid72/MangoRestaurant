using Mango.Services.OrderAPI.DbContexts;
using Mango.Services.OrderAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Mango.Services.OrderAPI.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DbContextOptions<ApplicationDbContext> _dbContext;
        public OrderRepository(DbContextOptions<ApplicationDbContext> dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> AddOrder(OrderHeader orderHeader)
        {
            try
            {
                await using var _db = new ApplicationDbContext(_dbContext);
                _db.OrderHeaders.Add(orderHeader);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }

        public async Task UpdatePaymebtStatus(int orderHeaderId, bool paid)
        {
            await using var _db = new ApplicationDbContext(_dbContext);
            var orderHederFromDb = await _db.OrderHeaders.FirstOrDefaultAsync(u => u.OrderHeaderId == orderHeaderId);
            if (orderHederFromDb != null)
            {
                orderHederFromDb.PaymantStatus = paid;
                _db.OrderHeaders.Update(orderHederFromDb);
                await _db.SaveChangesAsync();
            }

        }
    }
}

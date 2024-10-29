using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using YumBlazor.Data;
using YumBlazor.Repository.IRepository;

namespace YumBlazor.Repository
{
    public class OrderHeaderRpository : IOrderHeaderRpository
    {
        private readonly ApplicationDbContext _context;

        public OrderHeaderRpository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<OrderHeader> CreateAsync(OrderHeader orderHeader)
        {
            orderHeader.OrderDate = DateTime.Now;
            await _context.OrderHeaders.AddAsync(orderHeader);
            await _context.SaveChangesAsync();
            return orderHeader;
        }

        public async Task<IEnumerable<OrderHeader>> GetAllAsync(string? userId = null)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                var orderHeaders= await _context.OrderHeaders.Where(u=>u.UserId==userId).ToListAsync();
                return orderHeaders;
            }
            return await _context.OrderHeaders.ToListAsync();
        }

        public Task<OrderHeader> GetAsync(int id)
        {
            var order = _context.OrderHeaders.Include(u=>u.OrderDetails).FirstOrDefaultAsync(u=>u.Id==id);
            return order;
        }

        public async Task<OrderHeader> UpdateStatusAsync(string? status, int orderId, string PaymentIntentId)
        {
            var order = await _context.OrderHeaders.FirstOrDefaultAsync(u=>u.Id==orderId);
            if (order is not null)
            {
                order.Status = status;
                if (!string.IsNullOrEmpty(PaymentIntentId))
                {
                    order.IntentId = PaymentIntentId;
                }
                await _context.SaveChangesAsync();
            }
            return order;
        }

        public async Task<OrderHeader> GetOrderBySessionIdAsync(string sessionId)
        {
            return await _context.OrderHeaders.FirstOrDefaultAsync(u => u.SessionId == sessionId.ToString());
        }
    }
}

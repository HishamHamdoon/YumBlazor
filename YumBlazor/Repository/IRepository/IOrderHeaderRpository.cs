using YumBlazor.Data;

namespace YumBlazor.Repository.IRepository
{
    public interface IOrderHeaderRpository
    {
        public Task<OrderHeader> GetAsync(int  id);
        public Task<OrderHeader> GetOrderBySessionIdAsync(string  sessionId);
        public Task<OrderHeader> CreateAsync(OrderHeader orderHeader);
        public Task<OrderHeader> UpdateStatusAsync(string? status, int orderId,string PaymentIntentId);
        public Task<IEnumerable<OrderHeader>> GetAllAsync(string? userId=null);
    }
}

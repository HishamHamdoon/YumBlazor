using YumBlazor.Data;

namespace YumBlazor.Repository.IRepository
{
    public interface IShoppingCartRepository
    {
        public Task<bool> UpdateCartAsync(string? userId,int product,int updatedBy);
        public Task<IEnumerable<ShoppingCart>> GetAllAsync(string? userId);
        public Task<bool> ClearCartAsync(string? Userid);
        public Task<int> GetTotalCartcountAsync(string? userId);
    }
}

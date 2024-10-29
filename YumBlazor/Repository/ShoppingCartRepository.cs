using Microsoft.EntityFrameworkCore;
using YumBlazor.Data;
using YumBlazor.Repository.IRepository;

namespace YumBlazor.Repository
{
    
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ApplicationDbContext _context;
        public ShoppingCartRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> ClearCartAsync(string? Userid)
        {
            var cartItem = await _context.ShoppingCarts.Where(u=>u.UserId == Userid).ToListAsync();
            if (cartItem != null)
            {
                 _context.ShoppingCarts.RemoveRange(cartItem);
            }
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<ShoppingCart>> GetAllAsync(string? userId)
        {
            return await _context.ShoppingCarts.Where(u=>u.UserId==userId).Include(u=>u.Product).ToListAsync();
        }

        public async Task<int> GetTotalCartcountAsync(string? userId)
        {
            int cartCount = 0;
            var cartItems  = await _context.ShoppingCarts.Where(u => u.UserId == userId).ToListAsync();
            foreach (var item in cartItems)
            {
                cartCount += item.Count;
            }
            return cartCount;
        }

        public async Task<bool> UpdateCartAsync(string? userId, int product, int updatedBy)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return false;
            }
            var cart = await _context.ShoppingCarts.FirstOrDefaultAsync(u =>  u.UserId == userId && u.ProductId == product );
            if (cart == null)
            {
                cart = new ShoppingCart
                {
                    UserId = userId,
                    Count = updatedBy,
                    ProductId = product
                };
                await _context.ShoppingCarts.AddAsync(cart);
            }
            else
            {
                cart.Count += updatedBy;
                if (cart.Count <= 0)
                {
                    _context.ShoppingCarts.Remove(cart);

                }
            }
            return  await _context.SaveChangesAsync() > 0;
        }
    }
}

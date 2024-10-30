using Microsoft.EntityFrameworkCore;
using YumBlazor.Data;
using YumBlazor.Repository.IRepository;

namespace YumBlazor.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private IWebHostEnvironment _webHostEnvironment;
        public ProductRepository(ApplicationDbContext applicationDbContext, IWebHostEnvironment webHostEnvironment)
        {
            _applicationDbContext = applicationDbContext;
            _webHostEnvironment = webHostEnvironment;   
        }
        public async Task<Product> CreateAsync(Product obj)
        {
            await _applicationDbContext.Products.AddAsync(obj);
            await _applicationDbContext.SaveChangesAsync();
            return obj;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var obj = await _applicationDbContext.Products.FirstOrDefaultAsync(c => c.Id == id);
            var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, obj.ImageUrl.TrimStart('/'));
            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }
            if (obj != null)
            {
                _applicationDbContext.Products.Remove(obj);
                await _applicationDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Product> GetAsync(int id)
        {
            var Product = await _applicationDbContext.Products.AsNoTracking().Include("Category").FirstOrDefaultAsync(c => c.Id == id);  
            if(Product != null)    
                return Product;
            return new Product();
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _applicationDbContext.Products.Include("Category").ToListAsync();
        }

        public async Task<Product> UpdateAsync(Product obj)
        {
            var objFromDb = await _applicationDbContext.Products.FirstOrDefaultAsync(u=>u.Id == obj.Id);
            if (objFromDb != null)
                objFromDb.Name = obj.Name;
                objFromDb.Price = obj.Price;
                objFromDb.Description = obj.Description;
                objFromDb.CategoryId = obj.CategoryId;
                _applicationDbContext.Products.Update(objFromDb);
                await _applicationDbContext.SaveChangesAsync();

            return new Product();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using YumBlazor.Data;
using YumBlazor.Repository.IRepository;

namespace YumBlazor.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public CategoryRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<Category> CreateAsync(Category obj)
        {
            await _applicationDbContext.Category.AddAsync(obj);
            await _applicationDbContext.SaveChangesAsync();
            return obj;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var obj = await _applicationDbContext.Category.FirstOrDefaultAsync(c => c.Id == id);
            if (obj != null)
            {
                _applicationDbContext.Category.Remove(obj);
                await _applicationDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Category> GetAsync(int id)
        {
            var category = _applicationDbContext.Category.FirstOrDefault(c => c.Id == id);  
            if(category != null)    
                return category;
            return new Category();
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _applicationDbContext.Category.ToListAsync();
        }

        public async Task<Category> UpdateAsync(Category obj)
        {
            var objFromDb = await _applicationDbContext.Category.FirstOrDefaultAsync(u=>u.Id == obj.Id);
            if (objFromDb != null)
                objFromDb.Name = obj.Name;
                _applicationDbContext.Category.Update(objFromDb);
                await _applicationDbContext.SaveChangesAsync();

            return new Category();
        }
    }
}

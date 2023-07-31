using BookShop.Data.Contexts;
using BookShop.Data.Interfaces;
using BookShop.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            IEnumerable<Category> categories = await _db.Categories.ToListAsync();
            return categories;
        }
        public async Task<int> InsertCategoryAsync(Category category)
        {
            try
            {
                await _db.Categories.AddAsync(category);
                await _db.SaveChangesAsync();
                int id = await _db.Categories.MaxAsync(s => s.Id);
                return id;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public async Task<bool> UpdateCategoryAsync(int categoryId, Category category)
        {
            var data = await _db.Categories.FirstOrDefaultAsync(s => s.Id == categoryId);

            if (data == null) return false;

            data.Name = category.Name;
            data.DisplayOrder = category.DisplayOrder;
            _db.Categories.Update(data);
            await _db.SaveChangesAsync();
            return true;
        }
        public async Task<Category> GetCategoryByIdAsync(int categoryId)
        {
            Category? data = await _db.Categories.FirstOrDefaultAsync(s => s.Id == categoryId);
            return data!;
        }
        public async Task<bool> DeleteCategoryAsync(int categoryId)
        {
            var data = await _db.Categories.FirstOrDefaultAsync(s => s.Id == categoryId);
            if (data == null) return false;
            _db.Remove(data);
            await _db.SaveChangesAsync();
            return true;

        }
    }
}

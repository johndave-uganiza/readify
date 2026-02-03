using Readify.Data;
using Readify.Models;
using Readify.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Readify.Repositories.Implementation
{
    public class CategoryRepository : ICategory
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        #region ReadAsync
        public async Task<IEnumerable<Category>> ReadAsync()
        {
            var categories = await _db.Categories.ToListAsync();

            return categories;
        }
        #endregion

        #region CreateAsync
        public async Task<Category> CreateAsync(Category category)
        {
            var newCategory = await _db.Categories.AddAsync(category);
            await _db.SaveChangesAsync();

            return category;
        }
        #endregion

        #region FindAsync
        public async Task<Category?> FindAsync(int intId)
        {
            var categoryForUpdate = await _db.Categories.FindAsync(intId);

            return categoryForUpdate;
        }
        #endregion

        #region DeleteAsync
        public async Task<Category?> DeleteAsync(int intId)
        {
            var category = await _db.Categories.FindAsync(intId);

            if (category == null) return null;

            _db.Categories.Remove(category);
            await _db.SaveChangesAsync();

            return category;
        }
        #endregion

        #region UpdateAsync
        public async Task<Category> UpdateAsync(Category category)
        {
            _db.Categories.Update(category);
            await _db.SaveChangesAsync();

            return category;
        }
        #endregion
    }
}

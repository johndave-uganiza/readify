using Readify.Models;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace Readify.Repositories.Abstract
{
    public interface ICategory
    {
        public Task<Category> CreateAsync(Category category);
        public Task<IEnumerable<Category>> ReadAsync();
        public Task<Category?> FindAsync(int intId);
        public Task<Category> UpdateAsync(Category category);
        public Task<Category?> DeleteAsync(int intId);
    }
}

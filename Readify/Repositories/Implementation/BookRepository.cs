using Readify.Data;
using Readify.Models;
using Readify.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Readify.Repositories.Implementation
{
    public class BookRepository : IBook
    {
        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #region ReadAsync
        public async Task<IEnumerable<Book>> ReadAsync()
        {
            return await _context.Books
                .Include(b => b.Category)
                .ToListAsync();
        }
        #endregion

        #region FindAsync
        public async Task<Book?> FindAsync(int intId)
        {
            return await _context.Books
                .Include(b => b.Category)
                .FirstOrDefaultAsync(b => b.intBookId == intId);
        }
        #endregion

        #region CreateAsync
        public async Task CreateAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region UpdateAsync
        public async Task UpdateAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region DeleteAsync
        public async Task DeleteAsync(int intId)
        {
            var book = await FindAsync(intId);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
        }
        #endregion
    }
}

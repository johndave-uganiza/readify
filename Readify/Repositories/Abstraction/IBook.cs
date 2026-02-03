using Readify.Models;

namespace Readify.Repositories.Abstract
{
    public interface IBook
    {
        Task<IEnumerable<Book>> ReadAsync();
        Task<Book?> FindAsync(int intId);
        Task CreateAsync(Book book);
        Task UpdateAsync(Book book);
        Task DeleteAsync(int intId);
    }
}

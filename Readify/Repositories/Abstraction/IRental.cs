using Readify.Models;

namespace Readify.Repositories.Abstract
{
    public interface IRental
    {
        Task<IEnumerable<Rental>> ReadAsync();
        Task<Rental?> FindAsync(int intId);
        Task CreateAsync(Rental rental);
        Task UpdateAsync(Rental rental);
        Task DeleteAsync(int intId);
    }
}

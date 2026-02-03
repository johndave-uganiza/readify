using Readify.Data;
using Readify.Models;
using Readify.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Readify.Repositories.Implementation
{
    public class RentalRepository : IRental
    {
        private readonly ApplicationDbContext _context;

        public RentalRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #region ReadAsync
        public async Task<IEnumerable<Rental>> ReadAsync()
        {
            return await _context.Rentals
                .Include(r => r.Book)
                .ToListAsync();
        }
        #endregion

        #region FindAsync
        public async Task<Rental?> FindAsync(int intId)
        {
            return await _context.Rentals.Include(r => r.Book).FirstOrDefaultAsync(r => r.intRentalId == intId);
        }
        #endregion

        #region CreateAsync
        public async Task CreateAsync(Rental rental)
        {
            _context.Rentals.Add(rental);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region UpdateAsync
        public async Task UpdateAsync(Rental rental)
        {
            _context.Rentals.Update(rental);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region DeleteAsync
        public async Task DeleteAsync(int intId)
        {
            var rental = await FindAsync(intId);
            if (rental != null)
            {
                _context.Rentals.Remove(rental);
                await _context.SaveChangesAsync();
            }
        }
        #endregion
    }
}

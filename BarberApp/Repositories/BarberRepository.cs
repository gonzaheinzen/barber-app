using BarberApp.EF;
using BarberApp.Interfaces;
using BarberApp.Services;
using Microsoft.EntityFrameworkCore;

namespace BarberApp.Repositories
{
    public class BarberRepository(MyDbContext dbContext) : IBarberRepository
    {
        public async Task<Barber?> GetByIdAsync(int id)
        {
            var query = await (from a in dbContext.Barbers
                               where a.BarberId == id && !a.IsDeleted
                               select a).FirstOrDefaultAsync();
            return query;
        }

        public async Task<List<Barber>> GetAllAsync()
        {
            var query = await (from a in dbContext.Barbers
                               where !a.IsDeleted
                               select a).ToListAsync();
            return query;
        }
    }
}

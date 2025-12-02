using BarberApp.EF;
using BarberApp.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BarberApp.Repositories
{
    public class ServiceRepository(MyDbContext dbContext) : IServiceRepository
    {
        public async Task<Service?> GetByIdAsync(int id)
        {
            var query = await (from s in dbContext.Services
                               where s.ServiceId == id && !s.IsDeleted
                               select s).FirstOrDefaultAsync();
            return query;
        }
    }
}

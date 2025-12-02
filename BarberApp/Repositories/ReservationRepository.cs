using BarberApp.EF;
using BarberApp.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BarberApp.Repositories
{
    public class ReservationRepository(MyDbContext dbContext) : IReservationRepository
    {

        public async Task<Reservation?> GetByIdAsync(int id)
        {
            var query = await (from r in dbContext.Set<Reservation>()
                               where r.ReservationId == id && !r.IsDeleted
                               select r).FirstOrDefaultAsync();
            return query;
        }


        public async Task<int> InsertAsync(Reservation reservation)
        {
            dbContext.Set<Reservation>().Add(reservation);
            await dbContext.SaveChangesAsync();
            return reservation.ReservationId;
        }
    }
}

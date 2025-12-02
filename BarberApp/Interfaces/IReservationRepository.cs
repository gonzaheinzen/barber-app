using BarberApp.EF;

namespace BarberApp.Interfaces
{
    public interface IReservationRepository
    {
        Task<Reservation?> GetByIdAsync(int id);
        Task<int> InsertAsync(Reservation reservation);
    }
}
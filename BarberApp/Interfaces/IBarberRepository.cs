using BarberApp.EF;

namespace BarberApp.Services
{
    public interface IBarberRepository
    {
        Task<Barber?> GetByIdAsync(int id);

        Task<List<Barber>> GetAllAsync();
    }
}
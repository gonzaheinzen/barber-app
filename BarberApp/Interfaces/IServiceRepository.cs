using BarberApp.EF;

namespace BarberApp.Interfaces
{
    public interface IServiceRepository
    {
        Task<Service?> GetByIdAsync(int id);
    }
}
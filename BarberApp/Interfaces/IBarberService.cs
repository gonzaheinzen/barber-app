using BarberApp.ModelsDTO;

namespace BarberApp.Interfaces
{
    public interface IBarberService
    {
        Task<List<BarberDTO>> GetAllBarbersAsync();
        Task<BarberDTO?> GetByIdAsync(int id);
    }
}
using BarberApp.EF;
using BarberApp.ModelsDTO;

namespace BarberApp.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<Appointment?> GetByIdAsync(int id);

        Task<int> InsertAsync(Appointment appointment);

        Task<List<Appointment>> GetByBarberAndDateAsync(int id, DateOnly date);

        Task<List<Appointment>> GetAllAsync();

        Task<List<Appointment>> GetByBarberAsync(int barberid);

        Task UpdateStatusAsync(int appointmentId, string newStatus);
    }
}
using BarberApp.EF;
using BarberApp.ModelsDTO;

namespace BarberApp.Interfaces
{
    public interface IAppointmentService
    {
        Task<AppointmentDTO> CreateAsync(AppointmentCreateDTO dto);

        Task<AppointmentDTO?> GetByIdAsync(int id);

        Task<List<AppointmentDTO>> GetAvailablesAsync();

        Task<List<AppointmentDTO>> GetAppointmentsByBarberAsync(int barberid);

        Task<ReservationDTO> ReserveAsync(int appointmentId, AppointmentReserveDTO dto);

        Task<List<AppointmentDTO>> GetByBarberAndDateAsync(int barberId, DateOnly date);
    }
}
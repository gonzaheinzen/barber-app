using BarberApp.EF;
using BarberApp.Interfaces;
using BarberApp.ModelsDTO;
using Microsoft.EntityFrameworkCore;

namespace BarberApp.Repositories
{
    public class AppointmentRepository(MyDbContext dbContext) : IAppointmentRepository
    {

        public async Task<Appointment?> GetByIdAsync(int id)
        {
            var query = await (from a in dbContext.Appointments
                               where a.AppointmentId == id && !a.IsDeleted
                               select a).FirstOrDefaultAsync();
            return query;
        }


        public async Task<int> InsertAsync(Appointment appointment)
        {
            dbContext.Appointments.Add(appointment);
            await dbContext.SaveChangesAsync();
            return appointment.AppointmentId;
        }


        public async Task<List<Appointment>> GetAvailableByBarberAndDateAsync(int barberId, DateOnly? date) 
        {
            var query = await (from a in dbContext.Appointments
                               where a.BarberId == barberId && a.Status == "Available" && !a.IsDeleted && (date == null || a.Date == date)
                               select a).ToListAsync();

            return query;

        }

        public async Task<List<Appointment>> GetByBarberAndDateAsync(int id, DateOnly date)
        {
            var query = await (from a in dbContext.Appointments
                               where a.BarberId == id && a.Date == date && !a.IsDeleted
                               select a).ToListAsync();
            return query;
        }

        public async Task<List<Appointment>> GetAllAsync()
        {
            var query = await (from a in dbContext.Appointments
                               where !a.IsDeleted
                               select a).ToListAsync();
            return query;

        }

        public async Task<List<Appointment>> GetByBarberAsync(int barberid)
        {
            var query = await (from a in dbContext.Appointments
                               where a.BarberId == barberid && !a.IsDeleted
                               select a).ToListAsync();
            return query;
        }

        public async Task UpdateStatusAsync(int appointmentId, string newStatus)
        {
            var appointment = await GetByIdAsync(appointmentId);

            if (appointment == null)
            {
                throw new ArgumentException("El turno no existe");
            }

            appointment.Status = newStatus;
            await dbContext.SaveChangesAsync(); 
        }
    }
}
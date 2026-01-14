using System.ComponentModel;
using System.Diagnostics;
using BarberApp.EF;
using BarberApp.Interfaces;
using BarberApp.ModelsDTO;
using Microsoft.AspNetCore.Mvc;

namespace BarberApp.Services
{
    public class AppointmentService(IBarberRepository _barberRepository, IAppointmentRepository _appointmentRepository, IServiceRepository _serviceRepository, IClientRepository _clientRepository, IReservationRepository _reservationRepository) : IAppointmentService
    {


        private async Task<bool> HasOverlap(int barberId, DateOnly date, TimeOnly start, TimeOnly end)
        {
            var sameDayAppointments =
                await _appointmentRepository.GetByBarberAndDateAsync(barberId, date);

            return sameDayAppointments
                .Any(a => start < a.EndTime && end > a.StartTime);
        }


        public async Task<AppointmentDTO> CreateAsync(AppointmentCreateDTO dto)
        {
            var barber = await _barberRepository.GetByIdAsync(dto.BarberId);
            if (barber is null)
            {
                throw new ArgumentException("El barbero no existe");
            }

            var service = await _serviceRepository.GetByIdAsync(dto.ServiceId);
            if (service is null)
                throw new ArgumentException("El servicio no existe");

            var endTime = dto.StartTime.AddMinutes(service.DurationMinutes);


            var overlap = await HasOverlap(dto.BarberId, dto.Date, dto.StartTime, endTime);

            if (overlap)
            {
                throw new InvalidOperationException("El turno se superpone con otro.");
            }

            var appointment = new Appointment
            {
                BarberId = dto.BarberId,
                ServiceId = dto.ServiceId,
                Date = dto.Date,
                StartTime = dto.StartTime,
                EndTime = endTime
            };

            appointment.AppointmentId = await _appointmentRepository.InsertAsync(appointment);

            return ToDto(appointment);
        }

        private AppointmentDTO ToDto(Appointment appointment)
        {
            return new AppointmentDTO
            {
                AppointmentId = appointment.AppointmentId,
                BarberId = appointment.BarberId,
                ServiceId = appointment.ServiceId,
                Date = appointment.Date,
                StartTime = appointment.StartTime,
                EndTime = appointment.EndTime
            };
        }

        public async Task<AppointmentDTO?> GetByIdAsync(int id)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(id);

            if (appointment == null)
                return null;

            return ToDto(appointment);

        }

        public async Task<List<AppointmentDTO>> GetAvailableByBarberAndDateAsync(int barberId, DateOnly? date) 
        {
            var availables = await _appointmentRepository.GetAvailableByBarberAndDateAsync(barberId, date);
            return availables.Select(a => ToDto(a)).ToList();
        }

        public async Task<List<AppointmentDTO>> GetAvailablesAsync()
        {
            var availables = await _appointmentRepository.GetAllAsync();
            return availables.Select(a => ToDto(a)).ToList();
        }


        public async Task<List<AppointmentDTO>> GetAppointmentsByBarberAsync(int barberid)
        {
            var appointments = await _appointmentRepository.GetByBarberAsync(barberid);

            return appointments.Select(a => ToDto(a)).ToList();
        }

        public async Task<ReservationDTO> ReserveAsync(int appointmentId, AppointmentReserveDTO dto)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(appointmentId);

            if (appointment is null)
                throw new ArgumentException("El turno no existe");

            if (appointment.Status != "available")
                throw new InvalidOperationException("El turno ya está reservado");

            var client = await _clientRepository.GetByPhoneAsync(dto.Phone);

            if (client is null)
            {
                client = new Client
                {
                    Name = dto.Name,
                    Surname = dto.Surname,
                    Phone = dto.Phone
                };

                client.ClientId = await _clientRepository.InsertAsync(client);
            }

            await _appointmentRepository.UpdateStatusAsync(appointmentId, "reserved");

            var reservation = new Reservation
            {
                AppointmentId = appointmentId,
                ClientId = client.ClientId
            };

            reservation.ReservationId = await _reservationRepository.InsertAsync(reservation);

            return new ReservationDTO
            {
                ReservationId = reservation.ReservationId,
                AppointmentId = reservation.AppointmentId,
                Name = client.Name,
                Surname = client.Surname,
                Phone = client.Phone,
                Date = appointment.Date,
                StartTime = appointment.StartTime,
                EndTime = appointment.EndTime
                
            };
        }

        public async Task<List<AppointmentDTO>> GetByBarberAndDateAsync(int barberId, DateOnly date)
        {
            var appointments = await _appointmentRepository.GetByBarberAndDateAsync(barberId, date);
            return appointments.Select(a => ToDto(a)).ToList();
        }


    }
}

using System.Runtime.CompilerServices;
using BarberApp.EF;
using BarberApp.Interfaces;
using BarberApp.ModelsDTO;
using BarberApp.Repositories;
using BarberApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarberApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppointmentController(IAppointmentService appointmentService) : ControllerBase
    {

        // GET /appointment/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await appointmentService.GetByIdAsync(id));
        }

        // POST /appointment
        [HttpPost]
        public async Task<IActionResult> Appointments(AppointmentCreateDTO dto)
        {
            var created = await appointmentService.CreateAsync(dto);

            // Devuelve 201 con header Location
            return CreatedAtAction(nameof(GetById), new { id = created.AppointmentId }, created);
        }

        [HttpGet("availables")]
        public async Task<IActionResult> GetAppointmentsAvailables()
        {
            var availables = await appointmentService.GetAvailablesAsync();

            return Ok(availables);
        }

        [HttpGet("barber/{barberId:int}")]
        public async Task<IActionResult> GetByBarberAndOptionalDate(int barberId, [FromQuery] DateOnly? date)
        {
            if (date is null)
            {
                var all = await appointmentService.GetAppointmentsByBarberAsync(barberId);
                return Ok(all);
            }
            var filtered = await appointmentService.GetByBarberAndDateAsync(barberId, date.Value);
            return Ok(filtered);
        }


        [HttpPut("{id}/reserve")]
        public async Task<IActionResult> Reserve(int id, [FromBody] AppointmentReserveDTO dto)
        {
            var reservation = await appointmentService.ReserveAsync(id, dto);
            return Created($"/reservations/{reservation.ReservationId}", reservation);
        }

    }
}

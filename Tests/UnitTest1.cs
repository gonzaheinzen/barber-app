using BarberApp.Controllers;
using BarberApp.EF;
using BarberApp.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Tests
{
    public class UnitTest1
    {
        [Fact]
        public async Task Appointment_GetById_OK()
        {

            // ARRANGE
            Appointment simulacion = new Appointment { AppointmentId = 1, BarberId = 1 };
            
            var id = 1;

            var mockrepo = new Mock<IAppointmentRepository>();
            mockrepo.Setup(r => r.GetById(id)).ReturnsAsync(simulacion);



            var controller = new AppointmentController(mockrepo.Object);

            // ACT
            var result = await controller.GetById(id);


            // ASSERT
            var okResult = Assert.IsType<OkObjectResult>(result);
            var appointment = Assert.IsType<Appointment>(okResult.Value);

            Assert.NotNull(appointment);
            Assert.Equal(id, appointment.AppointmentId);
          



        }

    }
}
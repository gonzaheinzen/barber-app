using BarberApp.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BarberApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BarberController(IBarberService barberService) : ControllerBase
    {

        // GET /barber/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await barberService.GetByIdAsync(id));
        }

        // GET /barber
        [HttpGet()]
        public async Task<IActionResult> GetAllBarbers()
        {
            return Ok(await barberService.GetAllBarbersAsync());
        }
    }
}

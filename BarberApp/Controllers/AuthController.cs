using BarberApp.Interfaces;
using BarberApp.ModelsDTO;
using Microsoft.AspNetCore.Mvc;

namespace BarberApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController(IAuthService authService) : ControllerBase
    {

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDTO>> Login(LoginRequestDTO model)
        {
            var response = await authService.LoginAsync(model);

            if(!response.Success)
            {
                return Unauthorized(response);
            }

            //This will add token to response header
            Response.Headers["Authorization"] = "Bearer " + response.Token;


            //This will add the token to response body
            return Ok(response);
        }
    }
}

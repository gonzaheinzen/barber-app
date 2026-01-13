using BarberApp.ModelsDTO;

namespace BarberApp.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDTO> LoginAsync(LoginRequestDTO login);
    }
}
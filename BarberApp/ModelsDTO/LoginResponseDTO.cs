namespace BarberApp.ModelsDTO
{
    public class LoginResponseDTO
    {
        public bool Success { get; }
        public string Message { get; }
        public string? Token { get; }

        public LoginResponseDTO(bool success, string message, string? token = null)
        {
            Success = success;
            Message = message;
            Token = token;
        }
    }

}

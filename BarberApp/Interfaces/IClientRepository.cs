using BarberApp.EF;

namespace BarberApp.Interfaces
{
    public interface IClientRepository
    {
        Task<Client?> GetByIdAsync(int id);
        Task<Client?> GetByPhoneAsync(string phone);
        Task<int> InsertAsync(Client client);
    }
}
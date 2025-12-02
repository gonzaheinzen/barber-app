using BarberApp.EF;
using BarberApp.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BarberApp.Repositories
{
    public class ClientRepository(MyDbContext dbContext) : IClientRepository
    {
        public async Task<Client?> GetByIdAsync(int id)
        {
            var query = await (from c in dbContext.Clients
                               where c.ClientId == id && !c.IsDeleted
                               select c).FirstOrDefaultAsync();
            return query;
        }

        public async Task<Client?> GetByPhoneAsync(string phone)
        {
            var query = await (from c in dbContext.Clients
                               where c.Phone == phone && !c.IsDeleted
                               select c).FirstOrDefaultAsync();
            return query;
        }

        public async Task<int> InsertAsync(Client client)
        {
            dbContext.Clients.Add(client);
            await dbContext.SaveChangesAsync();
            return client.ClientId;
        }

    }
}

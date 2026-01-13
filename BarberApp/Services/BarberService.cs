using BarberApp.Interfaces;
using BarberApp.ModelsDTO;

namespace BarberApp.Services
{
    public class BarberService(IBarberRepository _barberRepository) : IBarberService
    {

        public async Task<BarberDTO?> GetByIdAsync(int id)
        {
            var barber = await _barberRepository.GetByIdAsync(id);
         
            if (barber is null)
                return null;

            return new BarberDTO
            {
                BarberId = barber.BarberId,
                Name = barber.Name,
                Surname = barber.Surname
            };
        }

        public async Task<List<BarberDTO>> GetAllBarbersAsync()
        {
            var barbers = await _barberRepository.GetAllAsync();

            return barbers.Select(barber => new BarberDTO
            {
                BarberId = barber.BarberId,
                Name = barber.Name,
                Surname = barber.Surname
            }).ToList();
        }


    }
}

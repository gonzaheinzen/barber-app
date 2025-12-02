using BarberApp.EF;

namespace BarberApp.ModelsDTO
{
    public class BarberDTO
    {

        public int BarberId { get; set; }

        public string Name { get; set; } = null!;

        public string Surname { get; set; } = null!;

    }
}

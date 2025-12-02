namespace BarberApp.ModelsDTO
{
    public class AppointmentCreateDTO
    {
        public int BarberId { get; set; }

        public int ServiceId { get; set; }

        public DateOnly Date { get; set; }

        public TimeOnly StartTime { get; set; }
    }
}

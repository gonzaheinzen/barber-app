namespace BarberApp.ModelsDTO
{
    public class AppointmentDTO
    {
        public int AppointmentId { get; set; }
        public int BarberId { get; set; }

        public int ServiceId { get; set; }

        public DateOnly Date { get; set; }

        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }
    }
}

namespace BarberApp.ModelsDTO
{
    public class ReservationDTO
    {

        public int ReservationId { get; set; }
        public int AppointmentId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }
}

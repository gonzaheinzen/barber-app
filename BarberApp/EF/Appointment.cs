using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarberApp.EF;

[Table("Appointments")]
public partial class Appointment
{
    public int AppointmentId { get; set; }

    public int BarberId { get; set; }

    public int ServiceId { get; set; }

    public DateOnly Date { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public string Status { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public virtual Barber Barber { get; set; } = null!;

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public virtual Service Service { get; set; } = null!;
}

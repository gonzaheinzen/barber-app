using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarberApp.EF;


[Table("Services")]
public partial class Service
{
    public int ServiceId { get; set; }

    public int BarberId { get; set; }

    public string Name { get; set; } = null!;

    public sbyte DurationMinutes { get; set; }

    public decimal Price { get; set; }

    public decimal? Deposit { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual Barber Barber { get; set; } = null!;
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarberApp.EF;

[Table("Barbers")]
public partial class Barber
{
    public int BarberId { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? Phone { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
}

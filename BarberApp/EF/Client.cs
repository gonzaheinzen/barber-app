using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarberApp.EF;

[Table("Clients")]
public partial class Client
{
    public int ClientId { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}

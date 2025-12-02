using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarberApp.EF;

[Table("Reservations")]
public partial class Reservation
{
    public int ReservationId { get; set; }

    public int AppointmentId { get; set; }

    public int ClientId { get; set; }

    public DateTime ReservationDatetime { get; set; }

    public decimal TotalAmount { get; set; }

    public decimal DepositAmount { get; set; }

    public string? PayStatus { get; set; }

    public string? PayMethod { get; set; }

    public string? PaymentId { get; set; }

    public string? MpStatus { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Appointment Appointment { get; set; } = null!;

    public virtual Client Client { get; set; } = null!;
}

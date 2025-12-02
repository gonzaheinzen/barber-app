using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BarberApp.EF;

public partial class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<Barber> Barbers { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.AppointmentId).HasName("PRIMARY");

            entity.ToTable("APPOINTMENTS");

            entity.HasIndex(e => e.AppointmentId, "appointment_id").IsUnique();

            entity.HasIndex(e => e.BarberId, "barber_id");

            entity.HasIndex(e => e.ServiceId, "service_id");

            entity.Property(e => e.AppointmentId).HasColumnName("appointment_id");
            entity.Property(e => e.BarberId).HasColumnName("barber_id");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.EndTime)
                .HasColumnType("time")
                .HasColumnName("end_time");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.ServiceId).HasColumnName("service_id");
            entity.Property(e => e.StartTime)
                .HasColumnType("time")
                .HasColumnName("start_time");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'available'")
                .HasColumnType("enum('available','reserved','paid','cancelled')")
                .HasColumnName("status");

            entity.HasOne(d => d.Barber).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.BarberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("APPOINTMENTS_ibfk_1");

            entity.HasOne(d => d.Service).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("APPOINTMENTS_ibfk_2");
        });

        modelBuilder.Entity<Barber>(entity =>
        {
            entity.HasKey(e => e.BarberId).HasName("PRIMARY");

            entity.ToTable("BARBERS");

            entity.HasIndex(e => e.BarberId, "barber_id").IsUnique();

            entity.HasIndex(e => e.Email, "email").IsUnique();

            entity.Property(e => e.BarberId).HasColumnName("barber_id");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .HasColumnName("email");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("password_hash");
            entity.Property(e => e.Phone)
                .HasMaxLength(30)
                .HasColumnName("phone");
            entity.Property(e => e.Surname)
                .HasMaxLength(100)
                .HasColumnName("surname");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.ClientId).HasName("PRIMARY");

            entity.ToTable("CLIENTS");

            entity.HasIndex(e => e.ClientId, "client_id").IsUnique();

            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(30)
                .HasColumnName("phone");
            entity.Property(e => e.Surname)
                .HasMaxLength(100)
                .HasColumnName("surname");
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => e.ReservationId).HasName("PRIMARY");

            entity.ToTable("RESERVATIONS");

            entity.HasIndex(e => e.AppointmentId, "appointment_id");

            entity.HasIndex(e => e.ClientId, "client_id");

            entity.HasIndex(e => e.ReservationId, "reservation_id").IsUnique();

            entity.Property(e => e.ReservationId).HasColumnName("reservation_id");
            entity.Property(e => e.AppointmentId).HasColumnName("appointment_id");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.DepositAmount)
                .HasPrecision(10, 2)
                .HasColumnName("deposit_amount");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.MpStatus)
                .HasMaxLength(50)
                .HasColumnName("mp_status");
            entity.Property(e => e.PayMethod)
                .HasMaxLength(50)
                .HasDefaultValueSql("'Mercado Pago'")
                .HasColumnName("pay_method");
            entity.Property(e => e.PayStatus)
                .HasDefaultValueSql("'pending'")
                .HasColumnType("enum('pending','paid','cancelled')")
                .HasColumnName("pay_status");
            entity.Property(e => e.PaymentId)
                .HasMaxLength(100)
                .HasColumnName("payment_id");
            entity.Property(e => e.ReservationDatetime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("reservation_datetime");
            entity.Property(e => e.TotalAmount)
                .HasPrecision(10, 2)
                .HasColumnName("total_amount");

            entity.HasOne(d => d.Appointment).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.AppointmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RESERVATIONS_ibfk_1");

            entity.HasOne(d => d.Client).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RESERVATIONS_ibfk_2");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("PRIMARY");

            entity.ToTable("SERVICES");

            entity.HasIndex(e => e.BarberId, "barber_id");

            entity.HasIndex(e => e.ServiceId, "service_id").IsUnique();

            entity.Property(e => e.ServiceId).HasColumnName("service_id");
            entity.Property(e => e.BarberId).HasColumnName("barber_id");
            entity.Property(e => e.Deposit)
                .HasPrecision(10, 2)
                .HasColumnName("deposit");
            entity.Property(e => e.DurationMinutes).HasColumnName("duration_minutes");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");

            entity.HasOne(d => d.Barber).WithMany(p => p.Services)
                .HasForeignKey(d => d.BarberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SERVICES_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
